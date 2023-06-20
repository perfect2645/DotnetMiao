using Puzhou.common;
using Puzhou.login;
using Puzhou.session;
using Utils;

namespace Puzhou.search
{
    internal class MiaoContent : PuzhouContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_deatil?date=";

        public MiaoContent(PuzhouLogin user, string date) : base(baseUrl, user)
        {
            var dateEncode = UnicodeConverter.Encode(date);
            RequestUrl = $"{baseUrl}{dateEncode}&hos_id=10001&project_id=10001";
        }
    }
}
