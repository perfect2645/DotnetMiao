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
            AddExchangeItem(3230, 118, "洁柔卫生纸Face立体压花无芯卷纸70g30卷装");
            AddExchangeItem(3229, 118, "全棉柔巾 ·棉柔巾洗面巾洁面卷装45g平纹200mm200");
            //AddExchangeItem(3225, 118, "海天生抽酱油500ml+精制料酒800ml （中华老字号）");
        }

        private void TestInitMiaoshaList()
        {
            
        }

        private void InitMiaoshaList()
        {
            AddMiaoshaItem(3269, 157, 2069, "十月稻田磨米世家五常黑米1kg(磨米世家五常黑米1kg 绵软", $"{DateTimeUtil.GetToday()} 19:59:58");
            AddMiaoshaItem(3270, 157, 2069, "十月稻田磨米世家张家口燕麦1kg(磨米世家张家口燕麦1kg", $"{DateTimeUtil.GetToday()} 19:59:58");
            AddMiaoshaItem(3271, 157, 2069, "湖北秭归九月红果冻橙 新鲜当季橙子(口口爆汁 可以吸着吃的果", $"{DateTimeUtil.GetToday()} 19:59:57");
            AddMiaoshaItem(3273, 157, 2069, "16件套餐具，款式任选，出口级品质(出口欧美，欧美风16件套", $"{DateTimeUtil.GetToday()} 19:59:58");
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
