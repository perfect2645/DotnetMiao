using Jksx.common;
using Jksx.login;
using Jksx.session;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace Jksx.search
{
    internal class VaccineContent : JksxContent
    {
        private static string baseUrl = "https://uhapi.sxyygh.com/native/schedule/api/v1/vac/vaccine/list";
        public VaccineContent(JksxLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("params", BuildEncodeParams());
            AddContent("endec", "on");
            AddContent("token", "v110");
        }

        private string BuildEncodeParams()
        {
            var paramDic = new Dictionary<string, object>();
            paramDic.AddOrUpdate("currentPageNo", 1);
            paramDic.AddOrUpdate("userid", User.MainId);
            paramDic.AddOrUpdate("pageSize", 20);

            return Encotor.EncodeFromDic(paramDic);
        }
    }
}
