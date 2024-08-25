using System;
using System.Collections.Generic;
using Zhuzher.session;

namespace Zhuzher.search
{
    internal class SunActivityScence
    {
        public string SceneCode { get; set; }
        public string MatchParam { get; set; }

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
            TestSceneList();
        }

        private void TestSceneList()
        {
            //到家服务结单后评价
            AddScene("5thzone-convenience-comment", 1, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=jifen");
        }

        private void InitSceneList()
        {
            AddScene("app-startapp", 1, "1");
            //登录分享
            AddScene("app-checkin-share", 1, "", 3);
            //到家服务结单后评价
            AddScene("5thzone-convenience-comment", 1, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=jifen");
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
            AddScene("5thzone-blackpearl-comment", 2, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=jifen");
            // 二手闲置发帖
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant?from=jfrw");
            // 浏览朴邻品牌介绍
            AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=pulin");
            // 浏览研选家品牌介绍
            AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=yanxuan");
            //送一面锦旗给管家
            AddScene("app-point-exchangebanners", 1, "https://enterprise.4009515151.com/marketing-pages/pennant?from=index");

            // 活动特定
            //转发活动邀请3人助力
            AddScene("app-act-invite&view", 1, $"1");
            //给别人成功助力1次
            AddScene("app-act-help", 1, $"1");
            // 每天逛逛百万补贴会场，浏览10秒以上即可完成任务
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/6262?pageMark=bbyhzzzzebwbt6262sy");
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

        private void AddScene(string code, string matchParam)
        {
            ScenceList.Add(new SunActivityScence
            {
                SceneCode = code,
                SceneTimes = 1,
                MatchParam = matchParam,
                Url = "",
                Version = 3
            });
        }
    }
}
