using Puzhou.common;
using Puzhou.login;
using Puzhou.session;
using Utils;

namespace Puzhou.search
{
    internal class VaccineContent : GzjkContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_title?hos_id=10001&project_id=10001";

        public VaccineContent(GzjkLogin user) : base(baseUrl, user)
        {
        }
    }
}
