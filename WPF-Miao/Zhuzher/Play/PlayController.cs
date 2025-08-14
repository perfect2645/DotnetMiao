using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Utils.datetime;
using Utils.number;
using Zhuzher.Play;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class PlayController : HttpClientBase
    {
        #region Properties

        public bool IsReward { get; set; } = false;
        public int Number { get; set; } = -1;

        #endregion Properties
        public PlayController(HttpClient httpClient) : base(httpClient)
        {
        }

        public int ActivityPlay(UserProject user)
        {
            var content = new PlayContent(user);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return Number;
            }
            if (response.Body == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return Number;
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();
            if (code == "200" && msg == "success")
            {
                var root = response.JsonBody.RootElement;
                var result = root.GetProperty("result");
                Number = result.GetProperty("number").GetInt32();
                var goodName = result.GetProperty("goodName").GetString();
                if (string.IsNullOrEmpty(goodName))
                {
                    MainSession.PrintLogEvent.Publish(this, $"{user.UserName} - 没抽到 - {Number}:{goodName}");
                    return Number;
                }
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName} - 抽到了 - {Number}:{goodName}");
                if (IsReward)
                {
                    Thread.Sleep(1000);
                    //if (Number == 1)
                    //{

                    //}
                    //GetReward(content);
                }
            }
            else
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName} - 抽奖失败- {msg}");
            }
            return Number;
        }

        private void GetReward(PlayContent content)
        {
            var reward = new Reward()
            {
                Authorization = content.User.Authorization,
                MatchParam = "10jb",
                ProjectCode = content.User.ProjectCode,
                RequestId = DateTimeUtil.GetTimeStamp(),
                SceneCode = "puli-daka",
                UserId = content.User.UserId,
            };

            var rewardController = HttpServiceController.GetService<PlayRewardController>();
            rewardController.GetReward(reward);
        }
    }
}
