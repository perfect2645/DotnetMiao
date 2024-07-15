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

            //ScoreBets.Add(new ScoreBet(56, 9245, 500, "华夫饼机"));
            //ScoreBets.Add(new ScoreBet(56, 9246, 500, "华夫饼机"));
            //ScoreBets.Add(new ScoreBet(56, 9247, 500, "华夫饼机"));
            //ScoreBets.Add(new ScoreBet(56, 9248, 500, "华夫饼机"));
            //ScoreBets.Add(new ScoreBet(56, 9249, 100, "华为电视"));

        }
    }
}
