using Jiangbei.common;
using Jiangbei.login;
using Jiangbei.session;
using Utils;

namespace Jiangbei.search
{
    internal class VaccineContent : JiangbeiContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_title?hos_id=10001&project_id=10001";

        public VaccineContent(JiangbeiLogin user) : base(baseUrl, user)
        {
        }
    }
}
