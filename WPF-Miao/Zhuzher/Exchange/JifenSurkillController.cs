using HttpProcessor.Client;
using HttpProcessor.Content;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils.stringBuilder;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class JifenSurkillController : HttpClientBase
    {
        #region Properties

        private readonly ScoreItemList GoodList = new ScoreItemList();
        private readonly UserProjectList UserProjectList = new UserProjectList();

        #endregion Properties
        public JifenSurkillController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void ScoreSurkill(UserProject user, ScoreItem good)
        {
            var content = new JifenSurkillContent(user, good);
            content.BuildDefaultHeaders(Client);

            if (good.Status >= 2)
            {
                return;
            }
            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
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
            else if (msg == "好礼兑换未开始")
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
        private void PrintLog(UserProject user, ScoreItem good, string? msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"兑换奖品- {good.Log}, message:{msg}");
            ZhuzherSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
