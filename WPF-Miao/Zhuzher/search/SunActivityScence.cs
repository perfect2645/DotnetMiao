using System.Collections.Generic;

namespace Zhuzher.search
{
    internal class SunActivityScence
    {
        public string SceneCode { get; set; }

        public int SceneTimes { get; set; }

        public string Url { get; set; }
    }

    internal class SunActivityScenceList
    {
        public List<SunActivityScence> ScenceList = new List<SunActivityScence>();
        public SunActivityScenceList()
        {
            InitSceneList();
        }

        private void InitSceneList()
        {
            AddScene("app-act-invite&view", 3, "1");
            AddScene("app-act-help", 1, "1");
            //AddScene("doc-withhold&prepay12months", 1, "zze://vanke.com/common/reactnative?route=BillRoute/PayBills");
            //AddScene("doc-prepay12months", 1, "zze://vanke.com/common/reactnative?route=BillRoute/PayBills");
            //AddScene("doc-prepay12months", 1, "zze://vanke.com/common/reactnative?route=BillRoute/PayBills");
            //AddScene("doc-withhold&prepay24months", 1, "zze://vanke.com/common/reactnative?route=BillRoute/PayBills");
            //AddScene("doc-withhold&prepay36months", 1, "zze://vanke.com/common/reactnative?route=BillRoute/PayBills");
            AddScene("app-startapp", 1, "1");
            // 首次进入活动，金币+5，并额外获得5个夺宝机会
            // 最多10次 组队邀请好友，好友成功加入队伍，则算完成
            //AddScene("app-act-invite&jointeam", 1, "1");

            //住这儿友邻市集单笔消费100元以上，每日可完成1次
            //AddScene("5thzone-blackpearl-pay", 1, "https://uiis.4009515151.com/activityTopic/4666?pageMark=ptgejhd");

            //优惠下单购买油烟机清洗服务！每日可完成1次任务 "limitCount": 16,
            //AddScene("5thzone-convenience-pay", 1, "https://neighbor.4009515151.com/summoner-canary/orders/product-detail?pageMark=zzesjbyyjdp&goodsId=6617274");

            //在世界杯话题下发帖即可完成，每日1次，每次+2金币 "limitCount": 2,
            //AddScene("app-post", 1, "https://enterprise.4009515151.com/marketing-pages/forum/topicPage?id=224");

        }

        private void AddScene(string code, int time, string? url=null)
        {
            ScenceList.Add(new SunActivityScence
            {
                SceneCode = code,
                SceneTimes = time,
                Url = url
            });
        }
    }
}
