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
            //AddScene("app-act-invite&view", 3, "1");
            //AddScene("app-act-help", 1, "1");
            //AddScene("5thzone-blackpearl-pay", 1, "https://uiis.4009515151.com/activityTopic/4666?pageMark=ptgejhd");
            //AddScene("app-postlike-puli", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=213");

            //以下只跑一次
            AddScene("doc-prepay12months", 1, "zze://vanke.com/common/reactnative?route=BillRoute/PayBills");


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
