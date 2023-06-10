using B114.login;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

namespace B114.common
{
    internal class B114Controller : HttpClientBase
    {
        public B114Controller(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SaveSessionTime(HttpDicResponse response, B114Login user)
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
