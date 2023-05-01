using Tongzhou.common;
using Tongzhou.login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tongzhou.search
{
    internal class UserContent : TongzhouPostContent
    {
        public UserContent(TongzhouLogin user) : base("current", "account.baseUserMvcService", user)
        {
            BuildContent();
            BuildSignHeaders();
        }

        private void BuildContent()
        {

        }
    }
}
