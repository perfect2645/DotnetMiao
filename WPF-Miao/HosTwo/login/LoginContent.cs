using HosTwo.common;
using HosTwo.session;
using System.Text;
using Utils;

namespace HosTwo.login
{
    internal class LoginContent : HosTwoContent
    {
        private static string baseUrl = "https://mix.med.gzhc365.com/api/xcxcore/login";
        public LoginContent(HosTwoLogin user) : base(baseUrl, user)
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

