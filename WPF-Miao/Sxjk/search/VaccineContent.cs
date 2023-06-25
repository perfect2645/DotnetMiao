using Sxjk.common;
using Sxjk.login;
using Sxjk.session;
using Utils;

namespace Sxjk.search
{
    internal class VaccineContent : SxjkContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_title?hos_id=10001&project_id=10001";

        public VaccineContent(SxjkLogin user) : base(baseUrl, user)
        {
        }
    }
}
