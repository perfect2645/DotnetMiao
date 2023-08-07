using Jksx.common;
using Jksx.login;
using Jksx.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Jksx.login
{
    internal class UserContent : JksxContent
    {
        private static string baseUrl = "https://ljzyyapi.yuanbaodaojia.com/v1/family_list";
        public UserContent(JksxLogin user) : base(baseUrl, user)
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
