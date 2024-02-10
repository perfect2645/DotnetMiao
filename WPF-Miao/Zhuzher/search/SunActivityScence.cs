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
            //TestSceneList();
            InitSceneList();
        }

        private void TestSceneList()
        {

            //写一首拜年诗歌，发帖成功即可获得1次抽奖机会
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=556");
            //浏览男神女神评选活动10秒以上完成任务
            AddScene("app-browseweb", 1, "https://communityclass.onewo.com/#/?contributeId=Mjk=&vkSource=video");
            //浏览新春夺宝活动10秒以上完成任务
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=992&from=neirong");
            //点击跳转前往关注公众号后，点击回复消息中的链接跳回
            AddScene("puli-daka", 1, "zze://vanke.com/common/service/launch_miniProgram?program_id=gh_501eecfa8fff&path=%2Fpages%2FMilkBook%2Findex%3Furl%3Dhttps%3A%2F%2Fmp.weixin.qq.com%2Fs%2Fz0lWLJ8LGFeGAOLEpi1DuA");
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
            // 将报告分享给3个好友
            AddScene("app-act-share&view", 3, "", 3);
            // 浏览朴邻品牌介绍
            AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=pulin");
            // 浏览研选家品牌介绍
            AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=yanxuan");


            // 活动特定
            //参加年货节活动
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=992&from=jifen");
            //旧物还能这样改？
            AddScene("app-vote-threetimes", 1, "https://communityclass.onewo.com/#/?contributeId=MjY=&vkSource=video&from=xmas");

            // 下单特惠商品
            AddScene("5thzone-blackpearl-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/5747?pageMark=bbtgxxtsbzjxzt5747");

            // 分享页面
            AddScene("app-h5share", 1, "", 3);
            // 积分兑换锦旗送管家
            AddScene("app-point-exchangebanners", 1, "https://enterprise.4009515151.com/marketing-pages/pennant?from=index");

            //逛逛到家服务
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5869?pageMark=nianhuojieliulan");
            //逛逛年货市集
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5861?pageMark=bbtgyhzznhzt5861m");
            //去看看家门口的大集
            AddScene("app-browseweb", 1, "https://communityclass.onewo.com/#/doc?docId=MTAzMA==&vkSource=video&from=nhj");
            //查看我的年度报告
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/year2023?from=huodongrenwu");
            // 积分兑换锦旗送管家
            AddScene("app-point-exchangebanners", 3, "https://enterprise.4009515151.com/marketing-pages/pennant?from=index");
            //前往积分中心参与秒杀
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/integral?from=hd");
            //完成1次积分兑换
            AddScene("app-point-exchangegoods", 1, "https://enterprise.4009515151.com/marketing-pages/integral/goodDetail?exchangeGoodId=3193&exchangeId=118");
            // 到家年货市集抢6折券
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5869?pageMark=jifennhj");
            // 友邻年货市集享满减
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5861?pageMark=bbtgjfzxnhjzt5861");

            //写一首拜年诗歌，发帖成功即可获得1次抽奖机会
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=556");
            //浏览男神女神评选活动10秒以上完成任务
            AddScene("app-browseweb", 1, "https://communityclass.onewo.com/#/?contributeId=Mjk=&vkSource=video");
            //浏览新春夺宝活动10秒以上完成任务
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=992&from=neirong");
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
