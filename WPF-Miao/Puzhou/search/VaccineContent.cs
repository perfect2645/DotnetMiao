using Puzhou.common;
using Puzhou.login;
using Puzhou.session;
using Utils;

namespace Puzhou.search
{
    internal class VaccineContent : PuzhouContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/project/list/drop?hos_id=10001&open=2";

        public VaccineContent(PuzhouLogin user) : base(baseUrl, user)
        {
        }
    }
}
