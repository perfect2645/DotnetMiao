using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ScoreBets.Add(new ScoreBet(56, 8003, 10, "美的（Midea） 变频微波炉 20升大平板底盘 智能菜单"));
            ScoreBets.Add(new ScoreBet(56, 8004, 10, "长虹 养生壶CHG15D1(实用养生壶)"));
            ScoreBets.Add(new ScoreBet(56, 8005, 10, "美菱取暖器 MPNLC0801(倾倒断电·碳纤维发热·恒温"));
        }
    }
}
