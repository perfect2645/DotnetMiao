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
            ScoreBets.Add(new ScoreBet(56, 8431, 20, "Apple iPhone 15 支持移动联通电信5G"));

        }
    }
}
