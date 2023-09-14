using HttpProcessor.Client;
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
    internal class CollectSunController : HttpClientBase
    {

        #region Properties

        private readonly SunActivityScenceList ScenceList = new SunActivityScenceList();
        private readonly UserProjectList UserProjectList = new UserProjectList();

        private readonly object _lock = new object();

        #endregion Properties

        public CollectSunController(HttpClient httpClient) : base(httpClient)
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
            lock(_lock)
            {
                foreach (var scene in ScenceList.ScenceList)
                {
                    for (var i = 0; i < scene.SceneTimes; i++)
                    {
                        Task.Factory.StartNew(() => CollectSunForEachScene(user, scene));
                        Thread.Sleep(2000);
                    }
                }
            }
        }

        public void CollectSun(UserProject user, SunActivityScence scene)
        {
            for (var i = 0; i < scene.SceneTimes; i++)
            {
                Task.Factory.StartNew(() => CollectSunForEachScene(user, scene));
                Thread.Sleep(5000);
            }
        }

        private void CollectSunForEachScene(UserProject user, SunActivityScence scene)
        {
            var content = new CollectSunContent();
            content.AddHeader("Cookie", ZhuzherSession.Cookie);
            content.AddHeader("Authorization", user.Authorization);
            content.AddContent("userId", user.UserId);
            content.AddContent("projectCode", user.ProjectCode);
            content.AddContent("sceneCode", scene.SceneCode);
            var defaultUrl = @"https:\/\/uiis.4009515151.com\/fg_activity\/template?id=2180";
            content.AddContent("matchParam", scene.Url ?? defaultUrl);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                ZhuzherSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
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

            ZhuzherSession.PrintLogEvent.Publish(this, sb.ToString());
        }
    }
}
