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
            //AddExchangeItem(4327, 219, "鲁花");
            AddExchangeItem(4416, 223, "买一送一】中秋DIY花灯9款可选亲子互动，锻炼动手能力");
            AddExchangeItem(4417, 223, "福建红心柚子新鲜三红柚子当季水果红肉柚子时令鲜果");
            AddExchangeItem(4418, 223, "【心相印】经典原生木桨100抽3层软抽纸 8包/提");
            AddExchangeItem(4419, 223, "沃隆成长每日坚果");
            AddExchangeItem(4420, 223, "【八十八倉】万科乡村振兴甄选·兴化大米2.5/5kg");
            AddExchangeItem(4421, 223, "蒙牛特仑苏纯牛奶苗条装250ml12盒");

            //AddExchangeItem(3225, 118, "海天生抽酱油500ml+精制料酒800ml （中华老字号）");
        }

        private void TestInitMiaoshaList()
        {
            
        }

        private void InitMiaoshaList()
        {
            AddMiaoshaItem(4175, 215, 2203, "湿巾", $"{DateTimeUtil.GetToday()} 19:59:59");
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
