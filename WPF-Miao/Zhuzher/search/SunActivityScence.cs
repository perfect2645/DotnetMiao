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
            //InitSceneList();
        }

        private void TestSceneList()
        {
            //研选家装，免费咨询
            //AddScene("yanxuan-submit-information", 1, "https://mj.4009515151.com/hasaki/#/puliRequireSave?requireType=yanxuan&activityType=springGo&sceneCode=yanxuan-submit-information");

            ////邀请新客下单空调清洗
            //AddScene("invitee-new-oder", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?goodsId=6636101&pageMark=zhuzheer_product_scan");
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
            //关注邻居公众号
            AddScene("puli-daka", "gzh");

            // 活动特定
            //参与5月焕新家活动
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=1054&from=jifen");
            // 下单特惠商品
            AddScene("5thzone-blackpearl-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/5747?pageMark=bbtgxxtsbzjxzt5747");
            // 分享页面
            AddScene("app-h5share", 1, "", 3);
            //在活动会场浏览10秒
            AddScene("app-browseweb", 1, "1");
            //每日参与话题讨论，发帖成功后完成任务
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=585&from=hd");
            //逛逛到家服务会场
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?type=mini&id=1056&from=hd");

            // 购买5月精选团购好物
            AddScene("5thzone-blackpearl-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/6153?pageMark=bbtgyhzzmqjzt6153t");
            // 评价友邻订单
            AddScene("5thzone-blackpearl-comment", 1, "https://neighbor.4009515151.com/nova/order-list");
            // 购买空调清洗服务
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?goodsId=6636101&pageMark=zhuzheer_product_scan");

            // 完单后评价服务质量
            AddScene("5thzone-convenience-comment", 1, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=nvshenjie");
            // 看看最新的物业公告
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/notice/list?from=act");
            //参加1次积分抽奖
            AddScene("app-points", 3, "https://enterprise.4009515151.com/marketing-pages/integral/turnTable?from=huodong");
            //给邻居发布的好物点赞
            AddScene("gottalent-like", 3, "https://enterprise.4009515151.com/social-circle/talentShow/vote?activityId=13&from=mini&from=activity");
            //看看母亲节团购好物
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/6153?pageMark=bbtgyhzzmqjzt6153t");
            // 确认到家服务完单
            AddScene("5thzone-conv-user-confirm", 1, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=nvshenjie");
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
