using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.collectsun
{
    internal class JoinTeamController : HttpClientBase
    {
        public JoinTeamController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void JoinTeamAsync()
        {
            Task.Factory.StartNew(() => JoinTeam());
        }

        private void JoinTeam()
        {
            var userData = new UserProjectList();
            var user = userData.UserProjects.FirstOrDefault();

            var content = new JoinTeamContent(user);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return;
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();
            if (code == null || code != "200")
            {
                throw new HttpException($"失败：{msg}");
            }
            PrintLog(user, msg);
        }

        private void PrintLog(UserProject user, string? msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"User:{user.UserName}, message:{msg}");

            MainSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
