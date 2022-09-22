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

        private readonly SunActivityScenceList ScenceList = new SunActivityScenceList();
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
            content.AddHeader("Authorization", user.Authorization);
            content.AddContent("activityGameId", user.UserId);
            content.AddContent("projectCode", user.ProjectCode);
            content.AddContent("projectName", user.ProjectName);
            content.AddContent("gameGoodId", user.ProjectName);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                throw new HttpException($"{user.UserName}登录过期了");
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();
            if (code == null || code != "200")
            {

                throw new HttpException($"失败：{msg}");
            }
            PrintLog(user, scene, msg);
        }

        private void PrintLog(UserProject user, SunActivityScence scene, string? msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"User:{user.UserName}, scene:{scene.SceneCode}, message:{msg}");

            ZhuzherSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
