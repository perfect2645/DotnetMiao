using HttpProcessor.Container;
using System;
using System.Linq;
using Utils;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class GoodDetailContent : ZhuzherContent
    {
        private const string _url = "https://chaos.4009515151.com/market/api/activity/good/detail";
        public MiaoshaItem Good { get; set; }

        public GoodDetailContent(UserProject user, MiaoshaItem good) : base(_url, user)
        {
            Good = good;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("activityGameId", Good.ActivityGameId);
            AddContent("gameGoodId", Good.GameGoodId);
            AddContent("activityId", MainSession.ActivityId.ToInt());
        }
    }
}