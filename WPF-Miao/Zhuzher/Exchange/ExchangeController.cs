using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            Task.Factory.StartNew(() =>
            {
                var userList = new UserProjectList();

                var defaultUser = userList.UserProjects.FirstOrDefault();
                var defaultGood = GoodList.MiaoshaList.FirstOrDefault();
                WaitGoodAvailable(defaultUser, defaultGood);

                foreach (var user in userList.UserProjects)
                {
                    foreach(var good in GoodList.MiaoshaList)
                    {
                        Exchange(user, good);
                        Thread.Sleep(1000);
                    }
                }
            });
            //Task.Factory.StartNew(() => Exchange());
        }

        private void WaitGoodAvailable(UserProject user, MiaoshaItem good)
        {
            bool isGoodAvailable = false;
            var goodController = HttpServiceController.GetService<GoodDetailController>();
            while(!isGoodAvailable)
            {
                isGoodAvailable = goodController.GoodAvailable(user, good);
                if (isGoodAvailable)
                {
                    return;
                }
                Thread.Sleep(1000 * 60);
            }
        }

        private void Exchange(UserProject user, MiaoshaItem good)
        {
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
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();

            PrintLog(user, good, msg);
        }

        public void Seckill(UserProject user, MiaoshaItem good)
        {
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

            if (good.Status >= 2)
            {
                return;
            }
            MainSession.PrintLogEvent.Publish(this, $"{user.UserName}-秒杀 【{good.GoodName}】开始了");
            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return;
            }
            good.Status = 2;
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();
            if (msg == "已达个人上限")
            {
                // 中奖了
                good.Status = 3;
            }
            else if (msg.Contains("未开始"))
            {
                // 未开始
                good.Status = 0;
            }
            else if (code == "200")
            {
                // 中奖了
                good.Status = 3;
            }
            else if (code.ToInt() > 500)
            {
                // 结束了
                good.Status = 2;
            }

            PrintLog(user, good, msg);
        }

        private void PrintLog(UserProject user, MiaoshaItem good, string? msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{user.UserName} 兑换奖品- {good.Log}, message:{msg}");
            MainSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
