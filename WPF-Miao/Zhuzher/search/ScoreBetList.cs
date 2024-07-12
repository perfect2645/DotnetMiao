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

            ScoreBets.Add(new ScoreBet(56, 9221, 500, "烤箱"));
            ScoreBets.Add(new ScoreBet(56, 9222, 500, "烤箱"));
            ScoreBets.Add(new ScoreBet(56, 9223, 500, "烤箱"));
            ScoreBets.Add(new ScoreBet(56, 9224, 500, "烤箱"));

        }
    }
}
