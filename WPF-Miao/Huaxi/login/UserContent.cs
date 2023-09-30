using Huaxi.common;
using Huaxi.login;
using Huaxi.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;

namespace Huaxi.login
{
    internal class UserContent : HuaxiContent
    {
        private static string baseUrl = "https://mcpwxp.motherchildren.com/cloud/ppclient/cardservice/userorgancardList";
        public UserContent(HuaxiLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        protected override void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            AddContent("organCode", hosId);
            AddContent("appCode", Constants.AppCode);
            AddContent("channelCode", Constants.ChannelCode);
        }
    }
}
