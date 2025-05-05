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

        /// <summary>
        /// 2 : https://z.onewo.com/market/api/notice/scene
        /// {"userId":24521842,"matchParam":"https://enterprise.4009515151.com/fe-event-pages/marketing?id=1105&from=share&sharerId=11067046&projectCode=21020025","sceneCode":"app-h5share","projectCode":"44030999","requestId":"1728122839473"}
        /// 4 : https://z.onewo.com/market/api/notice/scene
        /// {"userId":11067046,"sceneCode":"app-checkin-share","requestId":"20241004121023","projectCode":"21020025"}
        /// </summary>
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
            //邀请10个邻居助力
            //AddScene("app-act-invite&view", 1, "1");


            ////参与免费刷新1平米
            //AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/subscribe/index?routeType=NHJ-QMSX&channelConfigCode=BCE00000026");
            //预约并完成家政服务
            AddScene("5thzone-conv-user-confirm", 1, "https://neighbor.4009515151.com/summoner-canary/category-goods?pageMark=12yzhcjdqxyl&categoryId=3&pageMarkId=4949&categoryName=%E5%AE%B6%E7%94%B5%E6%B8%85%E6%B4%97");

            // 社区活动报名成功
            AddScene("app-communityactivity-signup", 1, "https://neighbor.onewo.com/neighbor-activity-mobile/zhuzher/activities");

            // 购买家政保洁维修服务
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/home?pageMark=dfhdldyyl");
        }

        private void InitSceneList()
        {
            //完成每日积分签到
            AddScene("app-checkin", 1, "https://enterprise.4009515151.com/marketing-pages/integral/signin");
            AddScene("app-startapp", 1, "1");
            //登录分享
            AddScene("app-checkin-share", 1, "", 4);
            ////逛家政/维修
            //AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/home?pageMark=jzjfzxllsyrw");
            ////逛送水服务
            //AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=zhuzheer_integral_center");
            ////手机开门
            //AddScene("app-keyforfree", 1, "zze://vanke.com/property/opendoor/list");
            ////逛逛友邻市集
            //AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/home-with-back");
            //// 浏览朴邻品牌介绍
            //AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=pulin");
            //// 浏览研选家品牌介绍
            //AddScene("app-browseweb", 1, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=yanxuan");
            //// 逛逛百万补贴福利
            //AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/6262?pageMark=jifenzx");

            // 活动特定
            //转发活动到3个微信群
            AddScene("app-h5share", 3, "https://enterprise.4009515151.com/fe-event-pages/marketing?id=1150&from=share&sharerId=28566129&projectCode=21020025", 2);

            //预约免费墙面刷新服务
            AddScene("yanxuan-submit-information", 1, "https://enterprise.4009515151.com/fe-event-pages/marketing?type=mini&id=1150&code=vkcwcawqe8");

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
