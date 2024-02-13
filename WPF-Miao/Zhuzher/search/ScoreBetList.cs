using System.Collections.Generic;
using Zhuzher.Score;

namespace Zhuzher.search
{
    internal class ScoreBetList
    {

        public List<ScoreBet> ScoreBets { get; set; }
        public ScoreBetList() 
        {
            InitList();
        }

        private void InitList()
        {
            ScoreBets = new List<ScoreBet>();
            ScoreBets.Add(new ScoreBet(56, 8430, 20, "Apple AirPods (第三代) 无线蓝牙耳机"));

        }
    }
}
