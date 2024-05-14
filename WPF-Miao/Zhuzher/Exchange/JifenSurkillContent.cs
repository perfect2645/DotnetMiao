using HttpProcessor.Content;
using Utils.datetime;
using Utils.stringBuilder;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class JifenSurkillContent : OnewoContent
    {
        public ScoreItem ScoreItem { get; set; }
        public const string Url = "https://z.onewo.com/market/api/score/time/exchange";
        public JifenSurkillContent(UserProject user, ScoreItem scoreItem) : base(Url, user)
        {
            User = user;
            ScoreItem = scoreItem;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
        }

        private void BuildContent()
        {
            AddContent("exchangeGoodId", ScoreItem.ExchangeGoodId);
            AddContent("exchangeId", ScoreItem.ExchangeId);
            //AddContent("exchangeGoodTimeId", ScoreItem.ExchangeGoodTimeId);
            AddContent("projectCode", User.ProjectCode);
        }
    }
}
