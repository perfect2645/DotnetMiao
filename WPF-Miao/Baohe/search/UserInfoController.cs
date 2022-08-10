using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.search
{
    internal class UserInfoController : HttpClientBase
    {
        public UserInfoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserInfoAsync()
        {
            Task.Factory.StartNew(() => GetUserInfo());
        }

        private void GetUserInfo()
        {
            var url = "https://appoint.yihu.com/appoint/do/user/getUserInfo";
            var content = new UserInfoContent(url);
            content.AddHeader("Cookie", "YiHu_OpenId=eyJPcGVuSUQiOiJvQTNaQTBjRkhSVGF3Tzg0M1hUSlpwUWFCaEk4IiwiU2VjU3RyIjoiMjE0MzJFOUE4MzRENzM3REYyMzczRkQ3MUI5RDQ0MDYifQ%3D%3D; logintype=62; loginprovinceid=0; logincityid=0; loginid=oA3ZA0cFHRTawO843XTJZpQaBhI8; OpenID=oA3ZA0cFHRTawO843XTJZpQaBhI8; BaseDoctorUid=0; BaseUserType=0; LoginChannel=9000393; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNDg1MjgxNTMiLCJDYXJkTnVtYmVyIjoiMjA3ODYzMTU1NyIsIkxvZ2luSWQiOiJvQTNaQTBjRkhSVGF3Tzg0M1hUSlpwUWFCaEk4IiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiJGQTMxQ0VEMkU3NzE1NTYzNzU3ODkyMEU5RDRFNTU1OSJ9; TOKEN_D2A3F1116EACBC455D70F61215A4E4B5=2D137B05BA9D49F68322498D571A72AB");

            content.BuildDefaultHeaders(Client);

            HttpDicResponse userInfo = PostStringAsync(content).Result;
        }
    }
}
