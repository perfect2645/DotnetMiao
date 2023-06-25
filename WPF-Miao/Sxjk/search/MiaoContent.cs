using Sxjk.common;
using Sxjk.login;
using Sxjk.session;
using Utils;

namespace Sxjk.search
{
    internal class MiaoContent : SxjkContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_deatil?date=";

        public MiaoContent(SxjkLogin user, string date) : base(baseUrl, user)
        {
            var dateWithFormat = date.Replace(' ', '+');
            RequestUrl = $"{baseUrl}{dateWithFormat}&hos_id=10001&project_id=10001";
        }
    }
}
