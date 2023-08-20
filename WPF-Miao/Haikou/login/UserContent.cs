using Haikou.common;
using Haikou.login;
using Haikou.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Haikou.login
{
    internal class UserContent : HaikouContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/family_list";
        public UserContent(HaikouLogin user) : base(baseUrl, user)
        {
            BuildUrl();
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("page", 1);
            AddContent("token", User.Token);
        }
    }
}
