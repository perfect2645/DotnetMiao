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

            ScoreBets.Add(new ScoreBet(56, 9256, 500, "九阳(Joyoung)电炖锅"));
            ScoreBets.Add(new ScoreBet(56, 9257, 500, "九阳(Joyoung)电炖锅"));
            ScoreBets.Add(new ScoreBet(56, 9258, 500, "九阳(Joyoung)电炖锅"));
            ScoreBets.Add(new ScoreBet(56, 9259, 500, "九阳(Joyoung)电炖锅"));
            //ScoreBets.Add(new ScoreBet(56, 9249, 100, "华为电视"));

        }
    }
}
