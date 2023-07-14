using Dalian.login;
using Dalian.session;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Dalian.login
{
    internal class TokenController : HttpClientBase
    {
        public TokenController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<bool> GetTokenAsync(DalianLogin user)
        {
            return Task.Factory.StartNew(() => GetToken(user));
        }

        private bool GetToken(DalianLogin user)
        {
            try
            {
                var content = new TokenContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetToken - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("resCode").GetInt32();
                if (code != 101)
                {
                    MainSession.PrintLogEvent.Publish(this, $"{user.UserName}-获取Token失败: code={code}");
                    return false;
                }

                var msg = root.GetProperty("msg");

                if (code == 101)
                {
                    MainSession.PrintLogEvent.Publish(this, $"{user.UserName}-获取初始Token: code={code}, message={msg}");
                }

                var headers = response.Headers;

                return CheckSaveToken(headers, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}获取Token异常{ex.Message}");
                return false;
            }
        }

        private bool CheckSaveToken(Dictionary<string, object> headers, DalianLogin user)
        {
            if (!headers.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}-获取Token失败，headers empty");
                return false;
            }

            var setCookies = headers["Set-Cookie"] as string[];
            foreach(var cookie in setCookies)
            {
                var cookieDic = cookie.CookieToDic();
                var imed_session = cookieDic.GetString("imed_session");
                if (!string.IsNullOrEmpty(imed_session))
                {
                }
                var imed_session_tm = cookieDic.GetString("imed_session_tm");
                if (!string.IsNullOrEmpty(imed_session_tm))
                {
                }
            }

            return true;
        }
    }
}