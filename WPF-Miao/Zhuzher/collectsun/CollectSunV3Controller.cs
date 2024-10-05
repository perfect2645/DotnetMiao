using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.collectsun
{
    internal class CollectSunV3Controller : HttpClientBase
    {

        #region Properties

        private readonly SunActivityScenceList ScenceList = new SunActivityScenceList();
        private readonly UserProjectList UserProjectList = new UserProjectList();

        private readonly object _lock = new object();

        #endregion Properties

        public CollectSunV3Controller(HttpClient httpClient) : base(httpClient)
        {
        }

        public void CollectSunAsync(UserProject user, SunActivityScence scene)
        {
            Task.Factory.StartNew(() => CollectSun(user, scene));
        }

        public void CollectSun(UserProject user, SunActivityScence scene)
        {
            for (var i = 0; i < scene.SceneTimes; i++)
            {
                Task.Factory.StartNew(() => CollectSunForEachScene(user, scene));
                Thread.Sleep(3000);
            }
        }

        private void CollectSunForEachScene(UserProject user, SunActivityScence scene)
        {
            OnewoContent? content = null;
            if (scene.Version == 3)
            {
                content = new CollectSunV3Content(user, scene);
            }
            else if (scene.Version == 4)
            {
                content = new CollectSunV4Content(user, scene);
            }
            else
            {
                return;
            }
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
            PrintLog(user, scene, msg);
        }

        private void PrintLog(UserProject user, SunActivityScence scene, string? msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"User:{user.UserName}, scene:{scene.SceneCode}, message:{msg}");

            MainSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
