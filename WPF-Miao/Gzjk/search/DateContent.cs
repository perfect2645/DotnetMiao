using Gzjk.common;
using Gzjk.login;
using Gzjk.session;
using System;
using Utils;

namespace Gzjk.search
{
    internal class DateContent : GzjkContent
    {
        private static string baseUrl = "https://api.cn2030.com/sc/wx/HandlerSubscribe.ashx?act=GetCustSubscribeDateAll";

        public DateContent(GzjkLogin user) : base(baseUrl, user)
        {
            BuildUrl();
        }

        private void BuildUrl()
        {
            var pid = MainSession.PlatformSession.GetString(Constants.DeptId);
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var month = DateTime.Today.ToString("yyyyMM");
            RequestUrl = $"{baseUrl}&pid={pid}&id={hosId}&month={month}";
        }
    }
}
