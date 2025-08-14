using HttpProcessor.Content;
using Utils.datetime;
using Utils.stringBuilder;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Score
{
    internal class ScoreExchangeContent : HttpStringContent
    {
        public UserProject User { get; set; }
        public ScoreItem ScoreItem { get; set; }
        public const string Url = "https://chaos.4009515151.com/market/api/score/good/exchange";
        public ScoreExchangeContent(UserProject user, ScoreItem scoreItem) : base(Url)
        {
            User = user;
            ScoreItem = scoreItem;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "chaos.4009515151.com");
            AddHeader("Origin", "https://enterprise.4009515151.com");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 vanke_app_version/5.3.92 X_API_VERSION/20220902 vanke_app/zhuzher vanke_jsbridge_version/5.3.92");
            AddHeader("Authorization", User.Authorization);
            AddHeader("Referer", "https://enterprise.4009515151.com/");

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