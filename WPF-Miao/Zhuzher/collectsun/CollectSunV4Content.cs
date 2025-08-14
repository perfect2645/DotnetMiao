using HttpProcessor.Content;
using System;
using Utils.datetime;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.collectsun
{
    internal class CollectSunV4Content : OnewoContent
    {
        private const string _url = "https://z.onewo.com/market/api/notice/scene";

        public SunActivityScence Scene { get; }

        public CollectSunV4Content(UserProject user, SunActivityScence scene) : base(_url, user)
        {
            Scene = scene;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
        }

        private void BuildContent()
        {
            var nowTimestapm = DateTimeUtil.GetTimeStamp();
            AddContent("userId", User.UserId);
            AddContent("sceneCode", Scene.SceneCode);
            AddContent("requestId", nowTimestapm);
            AddContent("projectCode", User.ProjectCode);
        }

    }
}
