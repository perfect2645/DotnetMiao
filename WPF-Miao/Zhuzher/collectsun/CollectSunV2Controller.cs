using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.collectsun
{
    internal class CollectSunV2Controller : HttpClientBase
    {

        #region Properties

        private readonly SunActivityScenceList ScenceList = new SunActivityScenceList();
        private readonly UserProjectList UserProjectList = new UserProjectList();

        private readonly object _lock = new object();

        #endregion Properties

        public CollectSunV2Controller(HttpClient httpClient) : base(httpClient)
        {
        }

        public void CollectSunAsync()
        {
            foreach(var user in UserProjectList.UserProjects)
            {
                Task.Factory.StartNew(() => CollectSun(user));
            }
        }

        public void CollectSun(UserProject user)
        {
            foreach (var scene in ScenceList.ScenceList)
            {
                var apiVersion = scene.Version;
                var v1Controller = HttpServiceController.GetService<CollectSunController>();
                if (apiVersion == 1)
                {
                    v1Controller.CollectSun(user, scene);
                    continue;
                }
                var v3Controller = HttpServiceController.GetService<CollectSunV3Controller>();
                if (apiVersion == 3)
                {
                    v3Controller.CollectSunAsync(user, scene);
                    continue;
                }
                for (var i = 0; i < scene.SceneTimes; i++)
                {
  
                    Task.Factory.StartNew(() => CollectSunForEachScene(user, scene));
                    Thread.Sleep(3000);
                }
            }
        }

        private void CollectSunForEachScene(UserProject user, SunActivityScence scene)
        {
            var content = new CollectSunV2Content(user, scene);

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
