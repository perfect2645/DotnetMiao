using HttpProcessor.Client;
using HttpProcessor.Content;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.Play;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.vote
{
    internal class VoteController : HttpClientBase
    {
        public VoteController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void VoteAsync()
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() => Vote(user));
                }
            });

        }

        public bool Vote(UserProject user)
        {
            var rewardContent = new VoteContent(user);
            rewardContent.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(rewardContent, ContentType.Json).Result;
            if (response == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName} 投票失败");
                return false;
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();

            if (code == "200" && msg == "success")
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName} 投票成功");
                return true;
            }
            else
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName} 兑换失败");
                return false;
            }
        }
    }
}