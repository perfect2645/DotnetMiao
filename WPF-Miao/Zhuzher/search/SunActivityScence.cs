using System.Collections.Generic;

namespace Zhuzher.search
{
    internal class SunActivityScence
    {
        public string SceneCode { get; set; }

        public int SceneTimes { get; set; }

        public string Url { get; set; }
    }

    internal class SunActivityScenceList
    {
        public List<SunActivityScence> ScenceList = new List<SunActivityScence>();
        public SunActivityScenceList()
        {
            InitSceneList();
        }

        private void InitSceneList()
        {
            AddScene("app-startapp", 1, "1");
            AddScene("app-act-invite&view", 20, "1");
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=4yzzetchdtzs");
            AddScene("app-points", 1, "https://enterprise.4009515151.com/marketing-pages/integral/turnTable?from=hdApril");
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=333");
            AddScene("app-communityactivity-signup", 1, "https://neighbor.onewo.com/neighbor-activity-mobile/zhuzher/activityDetail?id=96295");

        }

        private void AddScene(string code, int time, string? url=null)
        {
            ScenceList.Add(new SunActivityScence
            {
                SceneCode = code,
                SceneTimes = time,
                Url = url
            });
        }
    }
}
