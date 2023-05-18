using Jian.common;
using Jian.session;
using System.Text;
using Utils;

namespace Jian.login
{
    internal class LoginContent : JianContent
    {
        private static string baseUrl = "https://mix.med.gzhc365.com/api/xcxcore/login";
        public LoginContent(JianLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("hisId", hosId);
            AddContent("platformId", hosId);
            AddContent("platformSource", 3);
            AddContent("subSource", 1);
            AddContent("code", User.Code);
            AddContent("silent", string.Empty);
            AddContent("login_access_token", string.Empty);
        }
    }
}

