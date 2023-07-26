using Gzjk.common;
using Gzjk.login;
using Gzjk.session;
using Utils;

namespace Gzjk.search
{
    internal class MiaoContent : GzjkContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_deatil?date=";

        public MiaoContent(GzjkLogin user, string date) : base(baseUrl, user)
        {
            var dateWithFormat = date.Replace(' ', '+');
            RequestUrl = $"{baseUrl}{dateWithFormat}&hos_id=10001&project_id=10001";
        }
    }
}
