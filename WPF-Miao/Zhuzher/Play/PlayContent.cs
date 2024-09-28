using HttpProcessor.Content;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class PlayContent : OnewoContent
    {
        public const string Url = "https://z.onewo.com/market/api/activity/play";
        public PlayContent(UserProject user) : base(Url, user)
        {
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddDeviceId();
        }

        private void BuildContent()
        {
            AddContent("activityGameId", 1694);
            AddContent("activityId", MainSession.ActivityId);
            AddContent("projectCode", User.ProjectCode);
            AddContent("projectName", User.ProjectName);
        }
    }
}
