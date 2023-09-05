using Shangyu.common;
using Shangyu.login;
using Shangyu.session;
using System.Text;
using Utils;

namespace Shangyu.search
{
    internal class UserContent : ShangyuContent
    {
        private static string baseUrl = "https://app.gocent.com.cn/unite/api/User/GetPatientInfo?hospitalCode=";
        public UserContent(ShangyuLogin user) : base(baseUrl, user)
        {
            RequestUrl = $"{BaseUrl}&phone={user.Phone}";
            BuildContent();
        }

        private void BuildContent()
        {
        }
    }
}
