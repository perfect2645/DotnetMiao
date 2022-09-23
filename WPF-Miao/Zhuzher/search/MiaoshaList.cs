using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;

namespace Zhuzher.search
{
    internal class MiaoshaItem : NotifyChanged
    {
        public string ActivityGameId { get; set; }
        public int GameGoodId { get; set; }
        public string GoodName { get; set; }
        public DateTime StartTime { get; set; }
        public int Number { get; set; }

        /*
         * 0: 未开始
         * 1: 正在进行
         * 2: 结束
         * 3: 中奖了
         */
        private int _status;
        public int Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyUI(() => Status);
            }
        }
        public int Group { get; set; }
        public string Display { 
            get
            {
                return $"{Group} - {StartTime.ToString("t")} - {GoodName}";
            }
        }

        public string Log
        {
            get
            {
                return $"{DateTimeUtil.GetNow()} - {Group} - {StartTime.ToString("t")} - {GoodName}";
            }
        }
    }

    internal class MiaoshaItemList
    {
        public List<MiaoshaItem> MiaoshaList { get; set; }

        public MiaoshaItemList()
        {
            MiaoshaList = new List<MiaoshaItem>();
            TestInitMiaoshaList();
            //InitMiaoshaList();
        }

        private void TestInitMiaoshaList()
        {
            AddMiaoshaItem(2859, 1, "AirPods 3", "466", "2022-09-23 22:50:10");

        }

        private void InitMiaoshaList()
        {
            //AddMiaoshaItem(2821, "友邻市集5元无门槛券", "466");
            //AddDuobaoItem(2817, "iPhone 14 Pro", "452", 72);
            //AddDuobaoItem(2818, "iPhone 14 Pro", "452", 72);
            AddMiaoshaItem(2832, 1, "AirPods 3", "466", "2022-09-25 12:00:00");
            AddMiaoshaItem(2835, 1, "长粒香东北大米5KG", "466", "2022-09-25 12:00:00");
            AddMiaoshaItem(2834, 1, "送一个月物业费", "466", "2022-09-25 12:00:00");
            AddMiaoshaItem(2841, 1, "5元物业券", "466", "2022-09-25 12:00:00");
            AddMiaoshaItem(2836, 2, "蒙牛圣牧有机奶1箱", "466", "2022-09-25 14:00:00");
            AddMiaoshaItem(2842, 2, "AirPods 3", "466", "2022-09-25 14:00:00");
            AddMiaoshaItem(2843, 2, "送一个月物业费", "466", "2022-09-25 14:00:00");
            AddMiaoshaItem(2844, 3, "AirPods 3", "466", "2022-09-25 16:00:00");
            AddMiaoshaItem(2845, 3, "送一个月物业费", "466", "2022-09-25 16:00:00");
            AddMiaoshaItem(2837, 3, "温氏牧场原味酸奶1箱", "466", "2022-09-25 16:00:00");
            AddMiaoshaItem(2833, 4, "一年物业费", "466", "2022-09-25 18:00:00");
            AddMiaoshaItem(2846, 4, "AirPods 3", "466", "2022-09-25 18:00:00");
            AddMiaoshaItem(2838, 4, "维达蓝色经典有芯卷纸", "466", "2022-09-25 18:00:00");
            AddMiaoshaItem(2819, 4, "10元物业券", "466", "2022-09-25 18:00:00");
            AddMiaoshaItem(2847, 5, "AirPods 3", "466", "2022-09-25 20:00:00");
            AddMiaoshaItem(2848, 5, "送一个月物业费", "466", "2022-09-25 20:00:00");
            AddMiaoshaItem(2839, 5, "荣事达亚摩斯304不锈钢小奶锅", "466", "2022-09-25 20:00:00");
            AddMiaoshaItem(2851, 5, "20元物业券", "466", "2022-09-25 20:00:00");
            AddMiaoshaItem(2824, 6, "送1个月物业费", "465", "2022-09-25 20:20:00");
            AddMiaoshaItem(2849, 7, "AirPods 3", "466", "2022-09-25 22:00:00");
            AddMiaoshaItem(2850, 7, "送一个月物业费", "466", "2022-09-25 22:00:00");
            AddMiaoshaItem(2840, 7, "心相印印花厨房纸巾8卷", "466", "2022-09-25 22:00:00");


        }

        private void AddMiaoshaItem(int gameGoodId, int group, string goodName, string activityGameId, string startTimeStr = "")
        {
            var startTime = DateTime.Today;
            if (!string.IsNullOrEmpty(startTimeStr))
            {
                startTime = Convert.ToDateTime(startTimeStr);
            }

            if (startTime < DateTime.Now)
            {
                startTime = DateTime.Today;
            }

            MiaoshaList.Add(new MiaoshaItem
            {
                GameGoodId = gameGoodId,
                Group = group,
                GoodName = goodName,
                ActivityGameId = activityGameId,
                Number = 0,
                StartTime = startTime,
            });
        }

        private void AddDuobaoItem(int gameGoodId, string goodName, string activityGameId, int number = 0)
        {
            MiaoshaList.Add(new MiaoshaItem
            {
                GameGoodId = gameGoodId,
                GoodName = goodName,
                ActivityGameId = activityGameId,
                Number = number,
                StartTime = DateTime.Today,
            });
        }

        public MiaoshaItem GetDefaultGood()
        {
            return MiaoshaList.FirstOrDefault();
        }
    }
}
