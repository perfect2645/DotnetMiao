using Kuerle.common;
using Kuerle.login;
using Kuerle.session;
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
            var vidKey = MainSession.PlatformSession.GetString(Constants.VidKey);
            AddContent("GUID", vidKey);
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
