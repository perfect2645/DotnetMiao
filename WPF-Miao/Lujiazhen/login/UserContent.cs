using Lujiazhen.common;
using Lujiazhen.login;
using Lujiazhen.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Lujiazhen.login
{
    internal class UserContent : LujiazhenContent
    {
        private static string baseUrl = "https://ljzyyapi.yuanbaodaojia.com/v1/family_list";
        public UserContent(LujiazhenLogin user) : base(baseUrl, user)
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
