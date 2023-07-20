using Kuerle.common;
using Kuerle.login;
using Kuerle.session;
using System.Collections.Generic;
using Utils;

namespace Kuerle.search
{
    internal class IndexContent : KuerleContent
    {
        private static string baseUrl = "https://bzjk.qiyingtian.com/Home/GetMetadata2?pageName=book&isMobile=false&v2=396107520";

        public IndexContent(KuerleLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
        }
    }
}
