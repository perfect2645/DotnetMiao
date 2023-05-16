using HosTwo.common;
using HosTwo.session;
using System.Text;
using Utils;

namespace HosTwo.login
{
    internal class LoginContent : HosTwoContent
    {
        private static string baseUrl = "https://mix.med.gzhc365.com/api/user/patientinfo";
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
            AddContent("patientId", User.PatientId);
            AddContent("login_access_token", User.LoginAccessToken);
        }
    }
}

