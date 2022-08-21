using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

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
            GetUserSummary(sessionItem);
            GetUserDetails(sessionItem);
        }
        private void GetUserSummary(ISessionItem sessionItem)
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
                throw new HttpException($"{Constant.ProjectName}:GetUserDetails-{url} failed", Constant.AccountSn);
            }
            sessionItem.Key = userid;
            BaoheSession.AddUserSession(sessionItem, userInfo.Body);
            sessionItem.PrintLogEvent.Publish(this, userInfo.Body);
        }

        private void GetUserDetails(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/registerInfo/getMemberList";
            var content = new MemberListContent(url, sessionItem);
            content.AddHeader("Cookie", content.BuildCookie());
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetUserDetails-{url} - {response.Body["Message"]}", "bad response");
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:GetUserDetails-{url} - Result is empty", "empty result");
            }
            AnalizeResult(result, sessionItem);
        }

        private void AnalizeResult(JsonElement jsonElement, ISessionItem sessionItem)
        {
            var memberList = JsonAnalysis.JsonToDicList(jsonElement);

            sessionItem.SessionDic.AddOrUpdate(Constant.MemberList, memberList);
        }
    }
}
