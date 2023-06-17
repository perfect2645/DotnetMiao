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
            AddScene("app-act-invite&view", 3, "1");
            AddScene("puli-daka", 1, "https://enterprise.4009515151.com/marketing-pages/integral");
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/home?pageMark=djsyyxhdxrq");
            AddScene("5thzone-convenience-pay", 1, "https://blackpearl.4009515151.com/interfaces/share/show/topic/new/f1d439d387744f9095db179ce86a1333?from=sys");
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/5127?pageMark=bbyhzzznzt5127");
            AddScene("app-act-share&view", 6, "1");
            AddScene("app-post", 2, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=384");
            AddScene("puli-daka", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=384");
            AddScene("app-keyforfree", 1, "zze://vanke.com/property/opendoor/list");
            AddScene("doc-property-pay", 1, "https://enterprise.4009515151.com/payment-center/home");
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
