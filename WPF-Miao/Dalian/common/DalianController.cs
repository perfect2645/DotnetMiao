using Dalian.login;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

namespace Dalian.common
{
    internal class DalianController : HttpClientBase
    {
        public DalianController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SaveSessionTime(HttpDicResponse response, DalianLogin user)
        {
            var headers = response.Headers;
            if (!headers.HasItem())
            {
                return;
            }

            var setCookies = headers["Set-Cookie"] as string[];
            foreach (var cookie in setCookies)
            {
                var cookieDic = cookie.CookieToDic();
                var imed_session_tm = cookieDic.GetString("imed_session_tm");
                if (!string.IsNullOrEmpty(imed_session_tm))
                {
                    user.SessionTime = imed_session_tm;
                }
            }
        }
    }
}
