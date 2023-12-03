using HttpProcessor.Container;
using System;
using System.Linq;
using Utils;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Play
{
    internal class PlayDetailContent : ZhuzherContent
    {
        private const string _url = "https://chaos.4009515151.com/market/api/activity/play/detail";
        public MiaoshaItem Good { get; set; }

        public PlayDetailContent(UserProject user, MiaoshaItem good) : base(_url, user)
        {
            Good = good;
            BuildContent();
        }

        private void BuildContent()
        {
            //AddContent("activityId", MainSession.ActivityId.ToInt());
            AddContent("activityId", 971);
            AddContent("projectCode", User.ProjectCode);
            AddContent("projectName", User.ProjectName);
        }
    }
}