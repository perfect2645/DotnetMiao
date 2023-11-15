using System;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Score
{
    internal class UnCollectedScoreContent : OnewoContent
    {
        private const string _url = "https://z.onewo.com/market/api/turntable/getScoreUnclaimedPoints";
        public UnCollectedScoreContent(UserProject user) : base(_url, user)
        {

        }

    }
}
