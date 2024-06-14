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
            AddExchangeItem(4091, 209, "鲁花");

            //AddExchangeItem(3225, 118, "海天生抽酱油500ml+精制料酒800ml （中华老字号）");
        }

        private void TestInitMiaoshaList()
        {
            
        }

        private void InitMiaoshaList()
        {
            AddMiaoshaItem(4092, 210, 2192, "湿巾", $"{DateTimeUtil.GetToday()} 20:00:00");
            //AddMiaoshaItem(3787, 205, 2191, "拖鞋", $"{DateTimeUtil.GetToday()} 19:59:58");
            //AddMiaoshaItem(3785, 204, 2190, "纸", $"{DateTimeUtil.GetToday()} 19:59:59");
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
