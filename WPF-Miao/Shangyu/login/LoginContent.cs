using Shangyu.common;
using Shangyu.session;
using System.Text;
using Utils;

namespace Shangyu.login
{
    internal class LoginContent : ShangyuContent
    {
        private static string baseUrl = "https://app.gocent.com.cn/unite/api/User/GocentToken?hospitalCode=";
        public LoginContent(ShangyuLogin user) : base(baseUrl, user)
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

