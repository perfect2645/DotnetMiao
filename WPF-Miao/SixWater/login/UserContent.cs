using SixWater.common;
using SixWater.login;
using SixWater.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace SixWater.login
{
    internal class UserContent : SixWaterContent
    {
        private static string baseUrl = "https://ljzyyapi.yuanbaodaojia.com/v1/family_list";
        public UserContent(SixWaterLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("page", 1);
            AddContent("token", User.Authorization);
        }
    }
}
