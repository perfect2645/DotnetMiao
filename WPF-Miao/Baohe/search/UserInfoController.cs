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

        public void GetUserInfo()
        {

        }
    }
}
