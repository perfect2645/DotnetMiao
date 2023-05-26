using Lzy.common;
using Lzy.session;
using System.Text;
using Utils;

namespace Lzy.login
{
    internal class LoginContent : LzyContent
    {
        private static string baseUrl = "https://mix.med.gzhc365.com/api/xcxcore/login";
        public LoginContent(LzyLogin user) : base(baseUrl, user)
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
            AddContent("code", User.Cookie);
            AddContent("silent", string.Empty);
            AddContent("login_access_token", string.Empty);
        }
    }
}

