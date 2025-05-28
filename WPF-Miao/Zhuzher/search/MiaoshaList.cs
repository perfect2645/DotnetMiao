using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
            //var time = DateTime.Now.AddSeconds(10).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //AddMiaoshaItem(9806, 1, "5元无门槛物业券", "1765", $"{time}");

            //time = DateTime.Now.AddSeconds(14).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //AddMiaoshaItem(9807, 1, "88元无门槛物业券", "1765", $"{time}");

        }

        private void InitMiaoshaList()
        {
            var misecond = NumberUtil.IntRandom(500, 900);
            //AddMiaoshaItem(9811, 1, "250物业费", "1767", $"{DateTimeUtil.GetToday()} 20:00:00.{misecond}");
            AddMiaoshaItem(9973, 1, "88元预缴物业费礼券", "1767", $"{DateTimeUtil.GetToday()} 20:00:00.{misecond}");
            //AddMiaoshaItem(9763, 1, "88元物业费优惠券", "1752", $"{DateTimeUtil.GetToday()} 20:00:00.{misecond}");
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
