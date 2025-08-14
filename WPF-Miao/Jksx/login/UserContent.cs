using Jksx.common;
using Jksx.login;
using Jksx.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Jksx.login
{
    internal class UserContent : JksxContent
    {
        private static string baseUrl = "https://uhapi.sxyygh.com/patient/app/commonpeople/getCommonpeoplesByharsuserId";
        public UserContent(JksxLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("params", BuildEncodeParams());
            AddContent("endec", "on");
            AddContent("token", Constants.Version);
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
