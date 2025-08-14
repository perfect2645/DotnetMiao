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

            ScoreBets.Add(new ScoreBet(56, 9286, 500, "纽曼1"));
            ScoreBets.Add(new ScoreBet(56, 9287, 500, "纽曼2"));
            ScoreBets.Add(new ScoreBet(56, 9288, 500, "纽曼3"));
            ScoreBets.Add(new ScoreBet(56, 9289, 500, "纽曼4"));
            //ScoreBets.Add(new ScoreBet(56, 9249, 100, "华为电视"));

        }
    }
}
