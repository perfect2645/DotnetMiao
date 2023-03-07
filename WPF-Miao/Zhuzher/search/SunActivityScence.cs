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
            //AddScene("puli-daka", 1, "https://enterprise.4009515151.com/marketing-pages/h5/activity?id=79");
            AddScene("app-act-invite&view", 10, "1");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=act");
            AddScene("app-keyforfree", 5, "zze://vanke.com/property/opendoor/list");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=726&from=act828");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/?pageMark=yxhd");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/home?pageMark=zzesywyj");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/forum/matchmakingCorner");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=305");
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
