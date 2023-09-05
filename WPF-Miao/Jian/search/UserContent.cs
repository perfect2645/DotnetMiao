using Jian.common;
using Jian.login;
using Jian.session;
using System.Text;
using Utils;

namespace Jian.search
{
    internal class UserContent : JianContent
    {
        private static string baseUrl = "https://app.gocent.com.cn/unite/api/User/GetPatientInfo?hospitalCode=";
        public UserContent(JianLogin user) : base(baseUrl, user)
        {
            RequestUrl = $"{BaseUrl}&phone={user.Phone}";
            BuildContent();
        }

        private void BuildContent()
        {
        }
    }
}
