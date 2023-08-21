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
        private static string baseUrl = "https://wx.hospite.com/hkfy/patient/listHealthCard";
        public UserContent(HaikouLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            var appId = MainSession.PlatformSession.GetString(Constants.Appid);

            AddContent("appid", appId);
            AddContent("version", "v0.0.1");
            AddContent("token", User.Token);
        }
    }
}
