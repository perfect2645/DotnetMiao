using HttpProcessor.Content;
using System;
using Utils.datetime;
using Zhuzher.search;

namespace Zhuzher.collectsun
{
    internal class CollectSunV3Content : HttpStringContent
    {
        private const string _url = "https://z.onewo.com/market/api/notice/scene";

        protected UserProject User { get; set; }
        public SunActivityScence Scene { get; }

        public CollectSunV3Content(UserProject user, SunActivityScence scene) : base(_url)
        {
            User = user;
            Scene = scene;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "z.onewo.com");
            AddHeader("Accept", "application/json");
            AddHeader("Authorization", User.Authorization);
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Origin", "https://enterprise.4009515151.com");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 16_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 xxxx2023071801 vanke_app_version/5.5.00 X_API_VERSION/20230718 vanke_app/zhuzher vanke_jsbridge_version/5.5.00");
            AddHeader("Referer", "https://enterprise.4009515151.com/");
            AddHeader("Connection", "keep-alive");
        }

        private void BuildContent()
        {
            var nowTimestapm = DateTimeUtil.GetTimeStamp();
            AddContent("userId", User.UserId);
            AddContent("matchParam", Scene.MatchParam);
            AddContent("sceneCode", Scene.SceneCode);
            AddContent("requestId", nowTimestapm);
            AddContent("projectCode", User.ProjectCode);
        }

    }
}
