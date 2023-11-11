using HttpProcessor.Client;
using HttpProcessor.Content;
using System.Linq;
using System.Net.Http;
using Zhuzher.session;
namespace Zhuzher.Play
{
    internal class PlayRewardController : HttpClientBase
    {
        public PlayRewardController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool GetReward(Reward reward)
        {
            var rewardContent = new PlayRewardContent(reward);
            rewardContent.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(rewardContent, ContentType.Json).Result;
            if (response == null)
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"兑换失败");
                return false;
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();

            if (code == "200" && msg == "success")
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"兑换成功");
                return true;
            }
            else
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"兑换失败");
                return false;
            }
        }
    }
}
