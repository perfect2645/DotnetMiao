using HttpProcessor.Content;
using System;
using Utils.datetime;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.collectsun
{
    internal class CollectSunV2Content : OnewoContent
    {
        private const string _url = "https://z.onewo.com/market/partner/notice/scene";

        public SunActivityScence Scene { get; }

        public CollectSunV2Content(UserProject user, SunActivityScence scene) : base(_url, user)
        {
            Scene = scene;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "z.onewo.com");
            AddHeader("Authorization", User.Authorization);
            AddHeader("X-AppKey", "6292E1891CA246EB9A2AABD9039C4F71");
            AddHeader("X-AppSecret", "C67F286E63D54F3995C3F6C6F52508ED");
            AddDeviceId();
        }

        private void BuildContent()
        {
            var nowTimestapm = DateTimeUtil.GetTimeStamp();
            AddContent("matchParam", Scene.Url);
            AddContent("userId", User.UserId);
            AddContent("sceneCode", Scene.SceneCode);
            AddContent("requestId", nowTimestapm);
            AddContent("projectCode", User.ProjectCode);
        }

    }
}
