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
    internal class ScoreItem : NotifyChanged
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

    internal class ScoreItemList
    {
        public List<ScoreItem> MiaoshaList { get; set; }

        public ScoreItemList()
        {
            MiaoshaList = new List<ScoreItem>();
            InitMiaoshaList();
            //TestInitMiaoshaList();
        }

        private void TestInitMiaoshaList()
        {
            //AddMiaoshaItem(5734, 1, "【6折】家政家电清洁大额补贴", "1059", "2022-12-08 20:13:00");
        }

        private void InitMiaoshaList()
        {
            //AddDuobaoItem(6177, "跨年抢头菜", "1239", 10);
            //AddMiaoshaItem(7465, 1, "泸州老窖小酒免费兑换券", "1387", $"{DateTimeUtil.GetToday()} 20:00:00");
            AddMiaoshaItem(7428, 1, "洁柔厨房纸巾4包兑换券", "1387", $"{DateTimeUtil.GetToday()} 20:00:00");
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

            MiaoshaList.Add(new ScoreItem
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
            MiaoshaList.Add(new ScoreItem
            {
                GameGoodId = gameGoodId,
                GoodName = goodName,
                ActivityGameId = activityGameId,
                Number = number,
                StartTime = DateTime.Today,
            });
        }

        public ScoreItem GetDefaultGood()
        {
            return MiaoshaList.FirstOrDefault();
        }
    }
}
