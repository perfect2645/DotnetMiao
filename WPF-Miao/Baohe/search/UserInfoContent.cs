using Baohe.constants;
using Baohe.session;
using HttpProcessor.Content;
using System.Net.Http;
using System.Text;

namespace Baohe.search
{
    internal class UserInfoContent : HttpStringContent
    {
        public UserInfoContent(string url) : base(url)
        {
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            AddHeader("Host", "appoint.yihu.com");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "https://appoint.yihu.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36");

            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            base.BuildDefaultHeaders(httpClient);
        }

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];
            var time = BaoheSession.PlatformSesstion[Constant.SessionTime];

            var refererTemplate = $"https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

            return refererTemplate;
        }

        public string BuildCookie()
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
