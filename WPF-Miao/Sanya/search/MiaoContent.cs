using Sanya.common;
using Sanya.login;
using Sanya.session;
using Utils;

namespace Sanya.search
{
    internal class MiaoContent : SanyaContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_deatil?date=";

        public MiaoContent(SanyaLogin user, string date) : base(baseUrl, user)
        {
            var dateWithFormat = date.Replace(' ', '+');
            RequestUrl = $"{baseUrl}{dateWithFormat}&hos_id=10001&project_id=10001";
        }
    }
}
