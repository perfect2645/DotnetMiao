using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.number;
using Utils.Rdom;
using Zhuzher.session;

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
            var time = DateTime.Now.AddSeconds(10).ToString("yyyy-MM-dd HH:mm:ss.fff");
            AddMiaoshaItem(9026, 1, "助威高考纯牛奶5箱0元券", "1595", $"{time}");
        }

        private void InitMiaoshaList()
        {
            var misecond = NumberUtil.IntRandom(500, 800);
            //AddMiaoshaItem(9026, 1, "助威高考纯牛奶5箱0元券", "1595", $"{DateTimeUtil.GetToday()} 19:59:59.{misecond}");


            //AddMiaoshaItem(9030, 1, "飞天茅台1瓶", "1598", $"{DateTimeUtil.GetToday()} 19:59:58");
            //AddMiaoshaItem(9031, 1, "清风抽纸4包免单", "1598", $"{DateTimeUtil.GetToday()} 20:00:01");


            //AddMiaoshaItem(9032, 1, "空调清洗免单", "1599", $"{DateTimeUtil.GetToday()} 10:00:00");
            //AddMiaoshaItem(9033, 1, "油烟机清洗免单", "1599", $"{DateTimeUtil.GetToday()} 10:00:00");
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
