using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhuzher.search
{
    internal class MiaoshaItem
    {
        public string ActivityGameId { get; set; }
        public int GameGoodId { get; set; }
        public string GoodName { get; set; }
        public DateTime StartTime { get; set; }


    }

    internal class MiaoshaItemList
    {
        public List<MiaoshaItem> MiaoshaList { get; set; }

        public MiaoshaItemList()
        {
            MiaoshaList = new List<MiaoshaItem>();
            InitMiaoshaList();
        }

        private void InitMiaoshaList()
        {
            AddMiaoshaItem()
        }

        private void AddMiaoshaItem(int gameGoodId, string activityGameId, string GoodName, DateTime startTime)
        {
            MiaoshaList.Add(new MiaoshaItem
            {
            });
        }
    }
}
