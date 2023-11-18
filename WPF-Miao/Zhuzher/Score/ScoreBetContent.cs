using HttpProcessor.Container;
using System;
using System.Linq;
using Utils;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Score
{
    internal class ScoreBetContent : ZhuzherContent
    {
        private const string _url = "https://chaos.4009515151.com/market/api/score/loot/bet";
        public ScoreBet ScoreBet { get; set; }

        public ScoreBetContent(UserProject user, ScoreBet scoreBet) : base(_url, user)
        {
            ScoreBet = scoreBet;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("scoreExchangeId", ScoreBet.ScoreExchangeId);
            AddContent("gameGoodId", ScoreBet.GameGoodId);
            AddContent("number", ScoreBet.Number);
        }
    }
}