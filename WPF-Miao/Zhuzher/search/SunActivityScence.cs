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
            //AddScene("app-act-new-invite&view", 3, "1");



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
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=979&from=jifenrenwu");

            // 分享活动到微信被好友访问，即得5个“雪球”
            AddScene("app-act-share&view", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=993&from=share");
            //发布一个闲置，即得“雪球”，每周1次
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/secondHandsWithWant?from=xmas");
            // 学学如何做圣诞美食
            AddScene("app-browseweb", 1, "https://communityclass.onewo.com/#/video?vkSource=video&courseId=MzU0&selectId=&from=xmas");
            // 关注公众号，回复“圣诞快乐”，戳链接得“雪球”
            AddScene("puli-daka", 1, "gzhhuifu", 2);
            // 浏览积分中心10s
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/integral?from=hd");
            // 完成1次积分兑换
            AddScene("app-point-exchangegoods", 1, "https://enterprise.4009515151.com/marketing-pages/integral/goodDetail?exchangeGoodId=3193&exchangeId=118");
            // 每天登录领2雪球
            AddScene("app-startapp", 1, "1");
            // 逛逛团购特惠
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5747?pageMark=bbtgyhzzbzjxzt5747");
            // 逛逛维修服务
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5750?pageMark=nmxfdjwnjyhfzqdtg");
            // 逛逛家政服务
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5370?pageMark=zzegbjjzzt1112y");
            // 逛逛订水服务
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zzehdttd");
            // 下单1次到家服务
            AddScene("5thzone-convenience-pay", 1, "https://enterprise.4009515151.com/marketing-pages/h5/activity?id=153&type=mini&from=gaobaiji");
            //旧物还能这样改？
            AddScene("app-vote-threetimes", 1, "https://communityclass.onewo.com/#/?contributeId=MjY=&vkSource=video&from=xmas");
            // 积分兑换锦旗送管家
            AddScene("app-point-exchangebanners", 1, "https://enterprise.4009515151.com/marketing-pages/pennant?from=hd");
            // 下单特惠商品
            AddScene("5thzone-blackpearl-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/5747?pageMark=bbtgxxtsbzjxzt5747");
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
