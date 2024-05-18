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
            ScoreBets.Add(new ScoreBet(56, 8986, 100, "樱桃1"));
            ScoreBets.Add(new ScoreBet(56, 8987, 100, "樱桃2"));
            ScoreBets.Add(new ScoreBet(56, 8988, 100, "樱桃3"));
            ScoreBets.Add(new ScoreBet(56, 8989, 100, "樱桃4"));
            ScoreBets.Add(new ScoreBet(56, 8990, 100, "樱桃5"));
            ScoreBets.Add(new ScoreBet(56, 8991, 100, "樱桃6"));
            ScoreBets.Add(new ScoreBet(56, 8992, 100, "樱桃7"));
            ScoreBets.Add(new ScoreBet(56, 8993, 100, "樱桃8"));
            ScoreBets.Add(new ScoreBet(56, 8994, 100, "樱桃9"));
            ScoreBets.Add(new ScoreBet(56, 8995, 100, "樱桃10"));

        }
    }
}
