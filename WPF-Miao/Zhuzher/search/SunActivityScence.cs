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
        }

        private void InitSceneList()
        {
            AddScene("app-startapp", 1, "1");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/3895?pageMark=2023pljjzfw");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?pageMark=2023pljrcbj&goodsId=6630411");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?pageMark=2023pljyyj&goodsId=6633857");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?pageMark=2023pljcbl&goodsId=6632741");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5466?pageMark=eshsplj");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5492?pageMark=gdykjplj");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/summoner-canary/water?pageMark=pljhssy");
            AddScene("app-browseweb", 1, "https://neighbor.4009515151.com/nova/activityTopic/5498?pageMark=bbpljxcsxtht548zl");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/integral?from=hd");
            AddScene("app-browseweb", 1, "https://communityclass.onewo.com/#/doc?docId=MTAwMg==&vkSource=video");
            AddScene("app-browseweb", 1, "https://communityclass.onewo.com/#/video?vkSource=video&courseId=MzM4&selectId=");
            AddScene("app-browseweb", 1, "https://uiis.4009515151.com/fg_activity/template?id=2338");
            AddScene("app-browseweb", 2, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=yanxuan");
            AddScene("app-browseweb", 2, "https://mj.4009515151.com/hasaki/#/viewBrand?brandType=pulin");
            AddScene("app-browseweb", 2, "https://neighbor.4009515151.com/nova/activityTopic/4835?pageMark=bbsqysdjqrzt4835");
            AddScene("app-browseweb", 1, "https://enterprise.4009515151.com/marketing-pages/h5/activity?id=120");
            AddScene("app-browseweb", 1, "https://uiis.4009515151.com/fg_activity/template?id=2344");
            AddScene("app-browseweb", 2, "https://communityclass.onewo.com/#/doc?docId=MTAwMg==&vkSource=video");
            AddScene("230915-pulin-landingpage", 1, "https://mj.4009515151.com/hasaki/#/puliRequireSave?requireType=pulin&sceneCode=230915-pulin-landingpage");

            AddScene("app-act-invite&view", 3, "https://enterprise.4009515151.com/marketing-pages/puli2023?from=fenxiang");
            AddScene("app-keyforfree", 2, "zze://vanke.com/property/opendoor/list");

            AddScene("puli-daka", 1, "sqrenwu", 2);
            AddScene("puli-daka", 1, "gzh915", 2);

            AddScene("use-coupon-bonus", 1, "https://neighbor.4009515151.com/summoner-canary/home");


            AddScene("app-points", 1, "https://enterprise.4009515151.com/marketing-pages/integral/turnTable?from=hd");
            AddScene("app-points", 1, "https://enterprise.4009515151.com/marketing-pages/integral/loot?from=hd");
            AddScene("5thzone-integral-guessing", 1, " https://enterprise.4009515151.com/marketing-pages/guess?from=hd");
            AddScene("app-checkin", 1, "https://enterprise.4009515151.com/marketing-pages/integral/signin?from=hd");
            AddScene("app-point-exchangegoods", 3, "https://enterprise.4009515151.com/marketing-pages/integral/goodDetail?exchangeGoodId=1941&exchangeId=118");

            AddScene("5thzone-blackpearl-comment", 2, "https://neighbor.4009515151.com/nova/activityTopic/5498?pageMark=bbpljxcsxtht548zl");

            AddScene("use-coupon-bonus", 1, "https://neighbor.4009515151.com/summoner-canary/coupon-list ");

            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?pageMark=2023pljyyj&goodsId=6633857");
            AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/nova/activityTopic/5570?pageMark=jzpljxshd");



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
