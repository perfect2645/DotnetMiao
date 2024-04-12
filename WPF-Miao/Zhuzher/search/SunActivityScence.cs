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
            InitSceneList();
        }

        private void TestSceneList()
        {
            //参加女生节活动
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=1042&from=jifen");


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
            AddScene("5thzone-blackpearl-comment", 2, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=jifen");
            // 二手闲置发帖
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant?from=jfrw");
            // 浏览朴邻品牌介绍
            AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=pulin");
            // 浏览研选家品牌介绍
            AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=yanxuan");

            // 活动特定
            //参加女生节活动
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=1042&from=jifen");
            //旧物还能这样改？
            AddScene("app-vote-threetimes", 1, "https://communityclass.onewo.com/#/?contributeId=MjY=&vkSource=video&from=xmas");
            // 下单特惠商品
            AddScene("5thzone-blackpearl-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/5747?pageMark=bbtgxxtsbzjxzt5747");

            // 分享页面
            AddScene("app-h5share", 1, "", 3);
            // 浏览到家女神节活动页10秒即得10个OK币，每天1次
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=1018&type=youlin&from=jifen");
            // 评价友邻订单，获得100个OK币，可完成5次
            AddScene("5thzone-blackpearl-comment", 1, "https://neighbor.4009515151.com/nova/order-list");

            //在活动会场浏览10秒
            AddScene("app-browseweb", 1, "1");
            //加入同城闲置社群
            AddScene("puli-daka", 1, "https://enterprise.4009515151.com/fe-event-pages/association?community=1&id=10");
            //发布一次闲置
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant");
            //给手工活动投票3次
            AddScene("app-vote-threetimes", 3, "https://communityclass.onewo.com/#/contributeActivity?activityId=MzE=&vkSource=video&from=activity");
            //参加1次积分抽奖
            AddScene("app-points", 1, "https://enterprise.4009515151.com/marketing-pages/integral/turnTable?from=huodong");
            //分享活动到微信
            AddScene("app-h5share", 1, "", 3);
            //分享活动到微信
            AddScene("app-act-invite&view", 3, "1");
            //逛逛蕉下户外专场
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/6047?pageMark=hd4y");
            //逛逛喝水日专题
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zzehdzhc");
            //逛逛家政日专题
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5370?pageMark=4ysqyzhdrk32");
            //逛逛到家4月福利活动
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/6088?pageMark=zhuhuichangbiaos");
            //友邻购物晒1次好评
            AddScene("5thzone-blackpearl-comment", 1, "https://neighbor.4009515151.com/nova/order-list");

            //下单1次到家服务
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/6088?pageMark=zhuhuichangbiaos");
            //到家服务结单后评价
            AddScene("5thzone-convenience-comment", 1, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=nvshenjie");
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
