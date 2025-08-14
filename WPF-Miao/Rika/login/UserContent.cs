using Rika.common;
using Rika.login;
using Rika.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;

namespace Rika.login
{
    internal class UserContent : RikaContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/family_list";
        public UserContent(RikaLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("page", 1);
            var timestamp = DateTimeUtil.GetTimeStamp().Substring(0, 10);
            Content.AddOrUpdate("timestamp", timestamp);
            AddContent("token", User.Cookie);
        }
    }
}
