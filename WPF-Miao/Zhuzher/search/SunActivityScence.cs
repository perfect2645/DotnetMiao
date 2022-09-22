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
            AddScene("app-browseweb", 1, "https://uiis.4009515151.com/fg_activity/template?id=2180");
            AddScene("app-browseweb", 1, "https://sqmallh5.onewo.com/#/strollZhuzher?source=pulijie");
            AddScene("app-browseweb", 1, "https://uiis.4009515151.com/activityTopic/4455?pageMark=lhplzzehd");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/rightsCenter?from=pulijie");
            AddScene("app-browseweb", 1, "https://uiis.4009515151.com/activityTopic/3061?pageMark=22ndzxsjkm");
            AddScene("app-browseweb", 3, "https://pulin.vankeservice.com/weh5/#/pages/index/index?isAssetCard=1");
            AddScene("5thzone-convenience-pay", 3);
            AddScene("5thzone-mall-pay", 3);
            AddScene("5thzone-mall-verification", 5);
            AddScene("app-communityclassfinish", 1);
            AddScene("puli-booking", 1);
            AddScene("5thzone-integral-guessing", 1);
            AddScene("app-post", 1);
            AddScene("app-postlike-puli", 1);
            AddScene("pulin-retaincapital", 1);
            AddScene("app-h5share", 1, "https://enterprise.4009515151.com/marketing-pages/puliActivity");
            AddScene("app-update", 1);
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
