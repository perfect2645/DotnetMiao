using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.number;

namespace Zhuzher.search
{
    internal class ScoreItem : NotifyChanged
    {
        public string GoodName { get; set; }
        public int ExchangeGoodId { get; set; }
        public int ExchangeId { get; set; }
        public int ExchangeGoodTimeId { get; set; }
        public DateTime StartTime { get; set; }

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
        public List<ScoreItem> ExchangeList { get; set; }

        public ScoreItemList()
        {
            MiaoshaList = new List<ScoreItem>();
            ExchangeList = new List<ScoreItem>();
            InitMiaoshaList();
            InitExchangeList();
            //TestInitMiaoshaList();
        }

        private void InitExchangeList()
        {
            AddExchangeItem(5245, 279, "红包");
            //AddExchangeItem(4558, 283, "小麦粉");
            //AddExchangeItem(4560, 283, "蒙牛真果粒");
            //AddExchangeItem(4559, 231, "瓷碗");
        }

        private void TestInitMiaoshaList()
        {
            
        }

        private void InitMiaoshaList()
        {
            var misecond = NumberUtil.IntRandom(500, 900);
            AddMiaoshaItem(4441, 227, 2213, "50元物业预缴券（数量有限/每周一更新）", $"{DateTimeUtil.GetToday()} 11:59:59.{misecond}");
            //AddMiaoshaItem(4440, 227, 2213, "20元物业预缴券（数量有限/每周一更新）", $"{DateTimeUtil.GetToday()} 12:00:00");
            //AddMiaoshaItem(4439, 227, 2213, "10元物业预缴券（数量有限/每周一更新）", $"{DateTimeUtil.GetToday()} 12:00:00");
            //AddMiaoshaItem(4438, 227, 2213, "5元物业预缴券（数量有限/每周一更新）", $"{DateTimeUtil.GetToday()} 12:00:03");
        }

        private void AddMiaoshaItem(int exchangeGoodId, int exchangeId, int exchangeGoodTimeId, string goodName, string startTimeStr = "")
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
                ExchangeGoodId = exchangeGoodId,
                ExchangeGoodTimeId = exchangeGoodTimeId,
                ExchangeId = exchangeId,
                Group = 1,
                GoodName = goodName,
                StartTime = startTime,
            });
        }

        private void AddExchangeItem(int exchangeGoodId, int exchangeId, string goodName)
        {
            var startTime = DateTime.Today;

            if (startTime < DateTime.Now)
            {
                startTime = DateTime.Today;
            }

            ExchangeList.Add(new ScoreItem
            {
                ExchangeGoodId = exchangeGoodId,
                ExchangeId = exchangeId,
                Group = 1,
                GoodName = goodName,
                StartTime = startTime,
            });
        }

        public ScoreItem GetDefaultGood()
        {
            return MiaoshaList.FirstOrDefault();
        }
    }
}
