using Guangde.common;
using Guangde.login;
using Guangde.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Guangde.login
{
    internal class UserContent : GuangdeContent
    {
        private static string baseUrl = "https://ljzyyapi.yuanbaodaojia.com/v1/family_list";
        public UserContent(GuangdeLogin user) : base(baseUrl, user)
        {
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
