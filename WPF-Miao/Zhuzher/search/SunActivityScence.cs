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
            TestSceneList();
            //InitSceneList();
        }

        private void TestSceneList()
        {
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=jifen");
        }

        private void InitSceneList()
        {
            AddScene("app-startapp", 1, "1");
            //登录分享
            AddScene("app-checkin-share", 1, "", 3);
            //邻里互助发帖
            AddScene("app-post", 3, "https://enterprise.4009515151.com/marketing-pages/forum/portal?from=point");
            //订单评价
            AddScene("5thzone-blackpearl-comment", 2, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=jifen");
            //邻里互助评论
            AddScene("app-post-review", 1, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant?from=hd1108");
            //逛逛友邻集市
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5370?pageMark=sqxzjjzzt");
            //逛逛特惠商品 AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5468?pageMark=bbtghb11yzt5468mx");
            //手机开门
            AddScene("app-keyforfree", 1, "zze://vanke.com/property/opendoor/list");
            //逛逛家政
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/home?pageMark=jzjfzxllsyrw");
            //逛送水服务
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zhuzheer_integral_center");
            //积分话题发帖
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=358");
            //AddScene("app-post-review", 2, "https://neighbor.4009515151.com/nova/order-list");
            // 物业公告查看 AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=point");
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zzehdttd");
            // 到家服务单评价得积分
            // 二手闲置发帖
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant?from=jfrw");



            // 活动特定
            //参与感恩节活动
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=979&from=jifenrenwu");
            AddScene("app-point-exchangegoods", 1, "https://enterprise.4009515151.com/marketing-pages/integral/goodDetail?exchangeGoodId=3098&exchangeId=118");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/integral?from=hd");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zzehdttd");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5370?pageMark=zzegbjjzzt1112y");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5750?pageMark=nmxfdjwnjyhfzqdtg");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5747?pageMark=bbtgxxtsbzjxzt5747");
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zzehdttd");

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
