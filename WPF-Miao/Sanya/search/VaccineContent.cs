using Sanya.common;
using Sanya.login;
using Sanya.session;
using Utils;

namespace Sanya.search
{
    internal class VaccineContent : SanyaContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_title?hos_id=10001&project_id=10001";

        public VaccineContent(SanyaLogin user) : base(baseUrl, user)
        {
        }
    }
}
