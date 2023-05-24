using Kuerle.common;
using Kuerle.login;
using System.Collections.Generic;
using Utils;

namespace Kuerle.search
{
    internal class VidContent : KuerleContent
    {
        private static string baseUrl = "https://bzjk.qiyingtian.com/Home/CalcOdata";
        public string Pid { get; set; }

        public VidContent(KuerleLogin user, string pid) : base(baseUrl, user)
        {
            Pid = pid;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("GUID", "e69af53a-7a14-4e36-bb3e-0c741dcdf844");
            AddContent("PageName", "book");
            AddContent("Params", BuildParams());
        }

        private string[] BuildParams()
        {
            var paramsArr = new List<string>();
            paramsArr.Add($@"@'{Pid}'");

            return paramsArr.ToArray();
        }
    }
}
