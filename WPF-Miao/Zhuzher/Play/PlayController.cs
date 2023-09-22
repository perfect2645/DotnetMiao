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
    internal class PlayController : HttpClientBase
    {
        #region Properties

        private readonly ScoreItemList GoodList = new ScoreItemList();
        private readonly UserProjectList UserProjectList = new UserProjectList();

        #endregion Properties
        public PlayController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void ActivityPlay(UserProject user)
        {
            var content = new PlayContent(user);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return;
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();

            if (code == "200" && msg == "success")
            {
                var root = response.JsonBody.RootElement;
                var number = root.GetProperty("result").GetProperty("number").GetInt32();
                ZhuzherSession.PrintLogEvent.Publish(this, $"{user.UserName} - 抽到了 - {number}");
            }
            else
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"{user.UserName} - 抽奖失败- {msg}");
            }

        }
        private void PrintLog(UserProject user, string? msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"抽奖成功 - {user.UserName}, message:{msg}");
            ZhuzherSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
