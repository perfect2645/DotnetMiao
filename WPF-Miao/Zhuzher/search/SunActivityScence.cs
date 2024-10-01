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
            ////成功加入别人队伍
            //AddScene("app-act-jointeam", 1, "1");
            ////转发活动到微信群
            //AddScene("app-h5share", 1, "", 3);
            ////关注公众号每日领阳光
            AddScene("app-h5share", "https://enterprise.4009515151.com/fe-event-pages/marketing?type=mini&id=1105&scene=gzhanswer&from=gzhanswer");

            //关注公众号每日领阳光
            //AddScene("puli-daka", "gzhanswer");

            ////转发活动到微信群
            //AddScene("app-communityclassfinish", 1, "https://communityclass.onewo.com/#/video?vkSource=video&courseId=NDAx&selectId=");

        }

        private void InitSceneList()
        {
            //完成每日积分签到
            AddScene("app-checkin", 1, "https://enterprise.4009515151.com/marketing-pages/integral/signin");
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
            // 朴礼节签到
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?type=mini&id=1105&scene=hdchannel&from=jifen");
            //去看看社区未知的美好
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/social-circle/talentShow/vote?activityId=19&from=jifen");
            //到家服务结单后评价
            AddScene("5thzone-convenience-comment", 1, "https://neighbor.4009515151.com/summoner-canary/orders?tabIndex=0&isShowNavBar=1&pageMark=jifen");
            //邀请10个邻居助力
            AddScene("app-act-invite&view", 1, "1");
            //每天首次给别人助力
            AddScene("app-act-help", 1, "1");
            //完成每日积分抽奖
            AddScene("app-points", 1, "https://enterprise.4009515151.com/marketing-pages/integral/turnTable?from=huodong");
            //做达人随手拍赢物业费
            AddScene("gottalent-contribution", 1, "https://enterprise.4009515151.com/social-circle/talentShow/vote?activityId=19&from=pulijie");
            //关注你喜欢的达人
            AddScene("gottalent-fans", 1, "https://enterprise.4009515151.com/social-circle/talentShow/vote?activityId=19&from=pulijie");
            //给喜欢的达人作品点赞
            AddScene("gottalent-like", 1, "https://enterprise.4009515151.com/social-circle/talentShow/vote?activityId=19&from=pulijie");
            //谈记忆中妈妈做菜味道
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=585&from=activity");

            //说说身边美好的小事
            AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=587&from=activity");

            //发帖被点赞20次以上
            AddScene("app-postlike-puli", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=585&from=activity");

            //报名朴里节社区活动
            AddScene("app-communityactivity-signup", 1, "https://neighbor.onewo.com/neighbor-activity-mobile/zhuzher/activities");

            //朴里节社区活动签到
            AddScene("linjuhuodong", 1, "https://neighbor.onewo.com/neighbor-activity-mobile/zhuzher/activities");

            //逛逛百万补贴福利
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/6262?pageMark=bbyhzzzzebwbt6262hd");

            //购买朴里节严选好物
            AddScene("5thzone-blackpearl-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/6262?pageMark=bbyhzzzzebwbt6262hd");

            //购买桶装水或家政服务
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/6262?pageMark=bbyhzzzzebwbt6262hd");

            //预约家装服务顾问
            AddScene("yanxuan-submit-information", 1, "https://mj.4009515151.com/hasaki/#/save-data-index?routeType=ZZESY-PLJ&channelConfigCode=BCE00000026");

            //使用手机开门
            AddScene("app-keyforfree", 1, "zze://vanke.com/property/opendoor/list");

            //在活动会场停留10秒
            AddScene("app-browseweb", 1, "1");

            //转发活动到微信群
            AddScene("app-h5share", 1, "", 3);

            //关注公众号每日领阳光
            AddScene("puli-daka", "gzhanswer");
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
