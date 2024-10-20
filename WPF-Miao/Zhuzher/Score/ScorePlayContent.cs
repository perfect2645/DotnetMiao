using HttpProcessor.Content;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Score
{
    internal class ScorePlayContent : ZhuzherContent
    {
        public const string Url = "https://chaos.4009515151.com/market/api/turntable/play";
        public ScorePlayContent(UserProject user) : base(Url, user)
        {
            User = user;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {

        }

        private void BuildContent()
        {
            AddContent("scoreExchangeId", 25);
            AddContent("projectCode", User.ProjectCode);
            AddContent("projectName", User.ProjectName);
        }
    }
}
