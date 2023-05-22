using Anze.common;
using Anze.login;
using Anze.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Anze.login
{
    internal class UserContent : AnzeContent
    {
        private static string baseUrl = "https://yuyue.azjkzx.cn/api/index/getMemberInfo";
        public UserContent(AnzeLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("uid", User.UserId);
        }
    }
}
