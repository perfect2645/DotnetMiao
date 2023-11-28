using System;
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

            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=jifen");
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/portal?from=point");
            AddScene("5thzone-blackpearl-comment", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=jifen");

            AddScene("app-startapp", 1, "1");
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant?from=hd1108");
            AddScene("app-post-review", 5, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant?from=hd1108");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zzehdttd");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5370?pageMark=sqxzjjzzt");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5468?pageMark=bbtghb11yzt5468mx");
            AddScene("app-point-exchangegoods", 1, "https://enterprise.4009515151.com/marketing-pages/integral/goodDetail?exchangeGoodId=3098&exchangeId=118");
            AddScene("puli-daka", 1, "gzh915", 2);

            AddScene("app-keyforfree", 1, "zze://vanke.com/property/opendoor/list");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=971&from=jftask");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/home?pageMark=jzjfzxllsyrw");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zhuzheer_integral_center");
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant?from=jfrw");
            AddScene("app-post-review", 2, "https://neighbor.4009515151.com/nova/order-list");
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=358");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=jifen");
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/portal?from=point");
            AddScene("5thzone-blackpearl-comment", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=jifen");
            AddScene("gzhhuifu", 1, "1", 2);

            AddScene("app-mutuallike", 1, "https://enterprise.4009515151.com/marketing-pages/forum/matchmakingCorner");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/integral?from=hd");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zzehdttd");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5370?pageMark=zzegbjjzzt1112y");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5750?pageMark=nmxfdjwnjyhfzqdtg");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5747?pageMark=bbtgxxtsbzjxzt5747");
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zzehdttd");
            AddScene("app-checkin-share", 1, "" ,3);
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
