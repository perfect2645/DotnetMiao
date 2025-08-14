using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Play
{
    internal class PuliDrawPrizeContent : OnewoContent
    {
        private const string _url = "https://z.onewo.com/market/api/activity/team/good/get";

        public PuliDrawPrizeContent(UserProject user) : base(_url, user)
        {
            AddDeviceId();
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("activityId", MainSession.ActivityId);
            AddContent("projectCode", User.ProjectCode);
            AddContent("projectName", User.ProjectName);
        }
    }
}
