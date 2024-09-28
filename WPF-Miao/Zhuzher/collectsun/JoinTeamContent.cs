using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.collectsun
{
    internal class JoinTeamContent : OnewoContent
    {
        private const string _url = "https://z.onewo.com/market/api/activity/team/join";

        public JoinTeamContent(UserProject user) : base(_url, user)
        {
            AddDeviceId();
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("projectCode", User.ProjectCode);
            AddContent("activityId", MainSession.ActivityId);
            AddContent("inviteCode", MainSession.InviteCode);
        }
    }
}
