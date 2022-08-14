using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.search
{
    internal class UserInfoController : HttpClientBase
    {
        public UserInfoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetUserInfoAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => GetUserInfo(sessionItem));
        }

        private void GetUserInfo(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/user/getUserInfo";
            var content = new UserInfoContent(url);
            content.AddHeader("Cookie", BuildCookie());
            content.AddHeader("Referer", BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse userInfo = PostStringAsync(content).Result;
            var userid = userInfo.Body.FirstOrDefault(x => x.Key == Constant.AccountSn).Value?.ToString();
            if (userid == null || userid == "0")
            {
                throw new HttpException($"{Constant.ProjectName}:GetUserInfo-{url} failed", Constant.AccountSn);
            }
            sessionItem.Key = userid;
            BaoheSession.AddOrUpdate(sessionItem, userInfo.Body);
            sessionItem.PrintLogEvent.Publish(this, userInfo.Body);
        }

        private string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];
            var time = BaoheSession.PlatformSesstion[Constant.SessionTime];

            var refererTemplate = $"https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

            return refererTemplate;
        }

        private string BuildCookie()
        {
            var sb = new StringBuilder();
            sb.BuildSession(Constant.YihuOpenId);
            sb.BuildSession(Constant.LoginType);
            sb.BuildSession(Constant.LoginProvinceiId);
            sb.BuildSession(Constant.LoginCityId);
            sb.BuildSession(Constant.LoginId);
            sb.BuildSession(Constant.OpenId);
            sb.BuildSession(Constant.BaseDoctorUid);
            sb.BuildSession(Constant.BaseUserType);
            sb.BuildSession(Constant.LoginChannel);
            sb.BuildSession(Constant.YiHuUserJosn);
            sb.BuildSession(Constant.TOKEN);

            return sb.ToString();
        }
    }
}
