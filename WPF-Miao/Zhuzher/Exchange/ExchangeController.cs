using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils.datetime;
using Utils.stringBuilder;
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
            var user = UserProjectList.UserProjects.FirstOrDefault();
            var good = GoodList.GetDefaultGood();

            var isLoot = false;
            if (good.Number > 0)
            {
                isLoot = true;
            }

            var url = "https://chaos.4009515151.com/market/api/activity/good/exchange";
            if (isLoot)
            {
                url = "https://chaos.4009515151.com/market/api/activity/loot/exchange";
            }
            var content = new ExchangeContent(url);

            content.AddHeader("Authorization", user.Authorization);
            content.AddContent("projectCode", user.ProjectCode);
            content.AddContent("projectName", user.ProjectName);
            content.AddContent("activityGameId", good.ActivityGameId);
            content.AddContent("gameGoodId", good.GameGoodId);
            if (isLoot)
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


        public void Seckill(MiaoshaItem good)
        {
            var user = UserProjectList.UserProjects.FirstOrDefault();

            var isLoot = false;
            if (good.Number > 0)
            {
                isLoot = true;
            }

            var url = "https://chaos.4009515151.com/market/api/activity/good/exchange";
            if (isLoot)
            {
                url = "https://chaos.4009515151.com/market/api/activity/loot/exchange";
            }
            var content = new ExchangeContent(url);

            content.AddHeader("Authorization", user.Authorization);
            content.AddContent("projectCode", user.ProjectCode);
            content.AddContent("projectName", user.ProjectName);
            content.AddContent("activityGameId", good.ActivityGameId);
            content.AddContent("gameGoodId", good.GameGoodId);
            if (isLoot)
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
            if (code == "502")
            {
                // 未开始
                good.Status = 0;
            }
            else if (code == "200")
            {
                // 中奖了
                good.Status = 3;
            }
            else if (code.ToInt() >= 500)
            {
                // 结束了
                good.Status = 2;
            }

            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();
            PrintLog(user, good, msg);
        }

        private void PrintLog(UserProject user, MiaoshaItem good, string? msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{DateTimeUtil.GetNow()}兑换奖品- User:{user.UserName}, good:{good.GoodName}, message:{msg}");
            ZhuzherSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
