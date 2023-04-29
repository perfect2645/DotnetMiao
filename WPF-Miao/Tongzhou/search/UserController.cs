using HttpProcessor.Client;
using Tongzhou.common;
using Tongzhou.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Tongzhou.login;
using System;
using System.Collections.Generic;

namespace Tongzhou.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(TongzhouLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(TongzhouLogin user)
        {
            try
            {
                var content = new UserContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Array).Result;
                if (response?.ContentStr == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }

                var dicResultDe = Encrypt.Decrypt22(response?.ContentStr);
                var dicResult = JsonAnalysis.JsonToDic(dicResultDe);

                CheckSaveUser(dicResult, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private bool CheckSaveUser(Dictionary<string, object> dicResult, TongzhouLogin user)
        {
            var code = dicResult.GetString("code").ToInt();
            if (code != 200)
            {
                var msg = dicResult.GetString("msg");
                MainSession.PrintLogEvent.Publish(this, $"Get Userinfo Failed.code={code}, msg={msg}");
                return false;
            }


            return true;
            //MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
