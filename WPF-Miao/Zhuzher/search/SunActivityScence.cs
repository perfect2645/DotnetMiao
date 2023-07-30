using System.Collections.Generic;

namespace Zhuzher.search
{
    internal class SunActivityScence
    {
        public string SceneCode { get; set; }

        public int SceneTimes { get; set; }

        public string Url { get; set; }

        public int Version { get; set; } 
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
            //AddScene("puli-daka", 1, "zze://vanke.com/common/service/launch_miniProgram?program_id=gh_501eecfa8fff&path=pages%2FMilkBook%2Findex%3Furl%3Dhttps%3A%2F%2Fmp.weixin.qq.com%2Fs%2Fz0lWLJ8LGFeGAOLEpi1DuA", 2);
            AddScene("app-points", 1, "https://enterprise.4009515151.com/marketing-pages/integral/turnTable?from=hd0714");
            AddScene("app-post-vote", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=431&from=activity914");
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=430&from=activity");
            //AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=51hd");
            //AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/5370?pageMark=jzbjyxhd");
            //AddScene("5thzone-convenience-pay", 1, "https://uiis.4009515151.com/product/171910/projectCode/0?pageMark=bbyhzz17110tf");
            AddScene("doc-property-pay", 1, "https://enterprise.4009515151.com/payment-center/home");
            AddScene("app-playgames", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=918");
            AddScene("app-keyforfree", 1, @"zze://vanke.com/property/opendoor/list");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=act");
        }

        private void AddScene(string code, int time, string? url=null, int version = 1)
        {
            ScenceList.Add(new SunActivityScence
            {
                SceneCode = code,
                SceneTimes = time,
                Url = url,
                Version = version
            });
        }
    }
}
