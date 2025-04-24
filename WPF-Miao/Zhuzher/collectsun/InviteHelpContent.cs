using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.collectsun
{
    internal class InviteHelpContent : OnewoContent
    {
        private const string _url = "https://z.onewo.com/market/api/activity/invite/help";
        private UserProject _targetUser;
        private UserProject _sourceUser;
        public InviteHelpContent(UserProject targetUser, UserProject sourceUser) : base(_url, sourceUser)
        {
            _targetUser = targetUser;
            _sourceUser = sourceUser;
            AddDeviceId();
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("activityId", MainSession.ActivityId);
            AddContent("shareUserId", _targetUser?.UserId);
            AddContent("projectCode", _targetUser?.ProjectCode);
        }
    }
}
