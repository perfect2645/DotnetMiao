using Gzjk.common;
using Gzjk.login;
using Gzjk.session;
using Utils;

namespace Gzjk.search
{
    internal class MiaoContent : GzjkContent
    {
        private static string baseUrl = "https://api.cn2030.com/sc/wx/HandlerSubscribe.ashx?act=GetCustSubscribeDateDetail";

        public MiaoContent(GzjkLogin user, string date) : base(baseUrl, user)
        {
            var pid = MainSession.PlatformSession.GetString(Constants.DeptId);
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            RequestUrl = $"{baseUrl}&pid={pid}&id={hosId}&scdate={date}";
        }
    }
}
