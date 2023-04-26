using Tongzhou.common;
using Tongzhou.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tongzhou.search
{
    internal class UserContent : TongzhongPostContent
    {
        public UserContent(TongzhouLogin user) : base("familyMemberListWithOrganDS", "eh.familyMemberService", user)
        {
            BuildContent();
            BuildSignHeaders();
        }

        private void BuildContent()
        {
            AddContent("dataSource", "yuyue");
            AddContent("cardTag", string.Empty);
            AddContent("t", 1);
        }
    }
}
