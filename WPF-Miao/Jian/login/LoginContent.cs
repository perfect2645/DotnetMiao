using Jian.common;
using Jian.session;
using System.Text;
using Utils;

namespace Jian.login
{
    internal class LoginContent : JianContent
    {
        private static string baseUrl = "https://app.gocent.com.cn/unite/api/User/GocentToken?hospitalCode=";
        public LoginContent(JianLogin user) : base(baseUrl, user)
        {
            RequestUrl = $"{BaseUrl}";
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("phone", User.Phone);
            AddContent("pwd", User.Password);
        }
    }
}

