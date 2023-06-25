using Puzhou.common;
using Puzhou.login;
using Puzhou.session;
using Utils;

namespace Puzhou.search
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
