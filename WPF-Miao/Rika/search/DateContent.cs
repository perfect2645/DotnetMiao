using Rika.common;
using Rika.login;
using Rika.session;
using System;
using System.Text.Encodings.Web;
using Utils;
using Utils.datetime;
namespace Rika.search
{
    internal class DateContent : RikaContent
    {
        private static string baseUrl = "http://pyx.ygnetservice.cn/Wxgzh_Hospital//GzhHospitalYwService_JYFW/FUN_JYFW_YYGH_YS_List.action?";
        public int Index { get; set; }

        public DateContent(RikaLogin user, int index) : base(baseUrl, user)
        {
            Index = index;
            BuildUrl();
        }

        private void BuildUrl()
        {
            var ksID = MainSession.PlatformSession.GetString(Constants.DeptId);
            var ksMC = MainSession.PlatformSession.GetString(Constants.DeptName);
            var ksMCEncode = UnicodeConverter.Encode(ksMC, true);
            RequestUrl = $"{baseUrl}clientID={User.UserId}&ksID={ksID}&ksMC={ksMCEncode}&index={Index}";
        }
    }
}
