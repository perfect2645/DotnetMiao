using HttpProcessor.Container;
using System;
using System.Linq;
using Utils;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Score
{
    internal class CollectScoreContent : ZhuzherContent
    {
        private const string _url = "https://chaos.4009515151.com/market/api/turntable/getScore";
        public CollectScoreContent(UserProject user) : base(_url, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("idList", GetUncollectedScore());
        }

        private string[] GetUncollectedScore()
        {
            var result = new string[1] { "" };
            var unCollectedScoreController = HttpServiceController.GetService<UnCollectedScoreController>();

            var scoreDic = unCollectedScoreController.GetUnCollectedScore(User);
            if (!scoreDic.HasItem())
            {
                return result;
            }

            return scoreDic.Keys.ToArray();
        }
    }
}
