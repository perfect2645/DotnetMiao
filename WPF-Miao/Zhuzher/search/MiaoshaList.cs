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
            AddMiaoshaItem(4956, 1, "农夫山泉东北大米9斤", "943", "2022-11-11 17:00:00");
            AddMiaoshaItem(3645, 1, "blispring冰泉牙膏2支装", "629", "2022-11-11 00:14:03");
        }

        private void InitMiaoshaList()
        {
            //AddMiaoshaItem(5313, 1, "鲁花一级压榨花生油5L", "1059", "2022-11-27 20:00:00");
            AddMiaoshaItem(5314, 1, "荣事达水润负离子吹风机", "1059", "2022-11-27 20:00:00");
            AddMiaoshaItem(5312, 2, "美的智能按摩足浴盆", "1059", "2022-11-27 20:00:00");
            //AddMiaoshaItem(5302, 1, "百元大礼包 · 亲邻礼", "1057", "2022-11-18 20:00:00");

            //AddMiaoshaItem(2836, 2, "蒙牛圣牧有机奶1箱", "466", "2022-09-25 14:00:00");

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
