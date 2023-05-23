using Kuerle.common;
using Kuerle.login;
using Kuerle.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Kuerle.login
{
    internal class UserContent : KuerleContent
    {
        private static string baseUrl = "https://yuyue.azjkzx.cn/api/index/getMemberInfo";
        public UserContent(KuerleLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("uid", User.UserId);
        }
    }
}
