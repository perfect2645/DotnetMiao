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
            var timeNow = DateTime.Now.AddSeconds(20);
            AddMiaoshaItem(3105, 143, 1999, "特仑苏.08L ", $"{DateTimeUtil.GetToday()} 19:59:59");
            AddMiaoshaItem(3104, 143, 1999, "金典 ", $"{DateTimeUtil.GetToday()} 20:00:00");
            //AddMiaoshaItem(2590, 118, "诺梵低糖款松露巧克力500g/盒", $"{DateTimeUtil.GetToday()} 20:00:00");
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

        public ScoreItem GetDefaultGood()
        {
            return MiaoshaList.FirstOrDefault();
        }
    }
}
