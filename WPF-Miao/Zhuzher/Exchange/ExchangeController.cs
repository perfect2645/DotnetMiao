using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class ExchangeController : HttpClientBase
    {
        #region Properties

        private readonly MiaoshaItemList GoodList = new MiaoshaItemList();
        private readonly UserProjectList UserProjectList = new UserProjectList();

        #endregion Properties

        public ExchangeController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void ExchangeAsync()
        {
            Task.Factory.StartNew(() => Exchange());
        }

        private void Exchange()
        {
            var content = new ExchangeContent();

            var user = UserProjectList.UserProjects.FirstOrDefault();
            var good = GoodList.GetDefaultGood();

            content.AddHeader("Authorization", user.Authorization);
            content.AddContent("projectCode", user.ProjectCode);
            content.AddContent("projectName", user.ProjectName);
            content.AddContent("activityGameId", good.ActivityGameId);
            content.AddContent("gameGoodId", good.GameGoodId);
            if (good.Number > 0)
            {
                content.AddContent("number", good.Number);
            }
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();

            PrintLog(user, good, msg);
        }

        private void PrintLog(UserProject user, MiaoshaItem good, string? msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"兑换奖品- User:{user.UserName}, good:{good.GoodName}, message:{msg}");

            ZhuzherSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
