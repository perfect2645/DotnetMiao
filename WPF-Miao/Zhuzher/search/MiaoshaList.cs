using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                var threadId = Thread.CurrentThread.ManagedThreadId;
                return $"{DateTimeUtil.GetNow()} - Thread({threadId}) - {StartTime.ToString("t")} - {GoodName}";
            }
        }
    }

    internal class MiaoshaItemList
    {
        public List<MiaoshaItem> MiaoshaList { get; set; }

        public MiaoshaItemList()
        {
            MiaoshaList = new List<MiaoshaItem>();
            InitMiaoshaList();
            //TestInitMiaoshaList();
        }

        private void TestInitMiaoshaList()
        {
            //AddMiaoshaItem(5734, 1, "【6折】家政家电清洁大额补贴", "1059", "2022-12-08 20:13:00");
        }

        private void InitMiaoshaList()
        {
            AddMiaoshaItem(8110, 1, "【秒杀】插画款小水桶包", "1474", $"{DateTimeUtil.GetToday()} 20:00:01");
            //AddMiaoshaItem(8001, 1, "【天天兑】5元购物券", "1474", $"{DateTimeUtil.GetToday()} 10:00:00");
            //AddDuobaoItem(6177, "跨年抢头菜", "1239", 10);
            //AddMiaoshaItem(8108, 1, "【秒杀】新款升级魔术扫把", "1474", $"{DateTimeUtil.GetToday()} 20:00:05");
            //AddMiaoshaItem(8109, 1, "【秒杀】丁福衣物泡洗爆炸盐", "1474", $"{DateTimeUtil.GetToday()} 20:00:03");
            //AddMiaoshaItem(8106, 1, "【秒杀】防低头家用护颈神器", "1387", $"{DateTimeUtil.GetToday()} 20:00:00");
            //AddMiaoshaItem(8107, 1, "【秒杀】狮露洁衣物去油王", "1387", $"{DateTimeUtil.GetToday()} 20:00:02");
            //AddMiaoshaItem(8112, 1, "【秒杀】艾草颈椎矫正枕", "1387", $"{DateTimeUtil.GetToday()} 20:00:03");
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
