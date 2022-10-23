using Baohe.constants;
using Baohe.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.json;

namespace Baohe.search.user
{
    internal class UserInfoController : HttpClientBase
    {

        public List<Dictionary<string, object>> MemberList { get; private set; }

        public UserInfoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetUserInfoAsync()
        {
            return Task.Factory.StartNew(() => GetUserInfo());
        }

        private void GetUserInfo()
        {
            GetUserSummary();
            GetUserDetails();
        }
        private void GetUserSummary()
        {
            var url = "https://appoint.yihu.com/appoint/do/user/getUserInfo";
            var content = new UserInfoContent(url);
            content.AddHeader("Cookie", BaoheSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse userInfo = PostStringAsync(content).Result;
            var userid = userInfo.Body.FirstOrDefault(x => x.Key == Constant.accountSn).Value?.ToString();
            if (userid == null || userid == "0")
            {
                throw new HttpException($"{Constant.ProjectName}:GetUserDetails-{url} failed", Constant.accountSn);
            }
            BaoheSession.BuildUserSession(userid, userInfo.Body);
            BaoheSession.PrintLogEvent.Publish(this, userInfo.Body, "user summary");
        }

        private void GetUserDetails()
        {
            var url = "https://appoint.yihu.com/appoint/do/registerInfo/getMemberList";
            var content = new MemberListContent(url);
            content.AddHeader("Cookie", BaoheSession.Cookie);
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
            AnalysisResult(result);
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            MemberList = JsonAnalysis.JsonToDicList(jsonElement);

            BaoheSession.AddUserSession(Constant.MemberList, MemberList);

            BaoheSession.PrintLogEvent.Publish(this, MemberList, "user details");
        }
    }
}
