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
            AddMiaoshaItem(9040, 1, "桶装水100元神券", "1602", $"{time}");

            time = DateTime.Now.AddSeconds(14).ToString("yyyy-MM-dd HH:mm:ss.fff");
            AddMiaoshaItem(9038, 1, "桶装水60元神券", "1601", $"{time}");
        }

        private void InitMiaoshaList()
        {
            var misecond = NumberUtil.IntRandom(500, 800);
            AddMiaoshaItem(9168, 1, "小米手环8免单券", "1629", $"{DateTimeUtil.GetToday()} 19:59:58.{misecond}");
            AddMiaoshaItem(9169, 1, "随身小风扇免单券（运动避暑神器）", "1629", $"{DateTimeUtil.GetToday()} 20:00:02.{misecond}");
            //AddMiaoshaItem(9254, 1, "周ri10点抢无门槛券", "1648", $"{DateTimeUtil.GetToday()} 9:59:58.{misecond}");


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
