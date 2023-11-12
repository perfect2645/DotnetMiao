using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Utils.datetime;
using Zhuzher.Play;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class PlayController : HttpClientBase
    {
        #region Properties

        public bool IsReward { get; set; } = true;

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
                var result = root.GetProperty("result");
                var number = result.GetProperty("number").GetInt32();
                var goodName = result.GetProperty("goodName").GetString();
                if (string.IsNullOrEmpty(goodName))
                {
                    return;
                }
                ZhuzherSession.PrintLogEvent.Publish(this, $"{user.UserName} - 抽到了 - {number}:{goodName}");
                if (IsReward)
                {
                    Thread.Sleep(1000);

                    GetReward(content);
                }
            }
            else
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"{user.UserName} - 抽奖失败- {msg}");
            }

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
