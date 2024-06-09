using System;
using System.Collections.Generic;

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
            TestSceneList();
            InitSceneList();
        }

        private void TestSceneList()
        {
            //研选家装，免费咨询
            //AddScene("yanxuan-submit-information", 1, "https://mj.4009515151.com/hasaki/#/puliRequireSave?requireType=yanxuan&activityType=springGo&sceneCode=yanxuan-submit-information");
            //到家服务结单后评价
            //AddScene("app-housebinding", 1, "zze://vanke.com/user/house/list");
        }

        private void InitSceneList()
        {
            AddScene("app-startapp", 1, "1");
            //登录分享
            AddScene("app-checkin-share", 1, "", 3);
            //到家服务结单后评价
            AddScene("5thzone-convenience-comment", 1, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=nvshenjie");
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
            //参与活动免物业费
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=1065&from=jifen");
            //参加1次积分抽奖
            AddScene("app-points", 3, "https://enterprise.4009515151.com/marketing-pages/integral/turnTable?from=huodong");
            // 看看最新的物业公告
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=act");
            //分享遛娃圣地
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=591&from=activity");
            //点评短剧场抽按摩仪
            AddScene("app-post-review", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=592&from=activity");
            //逛逛友邻荔枝专题
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/6035?pageMark=bbtgyhhdsgzt6035t");
            //逛逛友邻牛奶专题
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/4998?pageMark=bbtgyhzznnzt48t");
            //友邻支付成功
            AddScene("5thzone-blackpearl-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/4998?pageMark=bbtgyhzznnzt48t");
            // 购买空调清洗服务
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?goodsId=6636101&pageMark=zhuzheer_product_scan");
            //邀请新客下单空调清洗
            AddScene("invitee-new-oder", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?goodsId=6636101&pageMark=zhuzheer_product_scan");
            //留言许愿成为锦鲤
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=596&from=activity");
            //分享你的狗屎运
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=597&from=activity");
            //完成任意金额在线缴费
            AddScene("doc-property-pay", 1, "https://enterprise.4009515151.com/payment-center/home");

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
