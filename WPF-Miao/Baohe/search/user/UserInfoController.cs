using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.search.user
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
            content.AddHeader("Cookie", content.BuildCookie());
            content.AddHeader("Referer", content.BuildReferer());

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
    }
}
