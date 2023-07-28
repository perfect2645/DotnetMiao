using Sanya.login;
using Sanya.session;
using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Sanya.common;

namespace Sanya.login
{
    internal class LoginController : HttpClientBase
    {
        public LoginController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task LoginAsync(SanyaLogin user)
        {
            return Task.Factory.StartNew(() => Login(user));
        }

        private void Login(SanyaLogin user)
        {
            try
            {
                //var zoeParams = JsReader.GetZoeParams(user.ZoeParams);
                var zoeParams = Encryptor.Base64Decode(user.ZoeParams);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"Login异常{ex.Message}");
            }
        }
    }
}
