using Base.viewmodel.status;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;
using Xihongmen.session;

namespace Xihongmen.login
{
    internal class LoginController : HttpClientBase
    {
        public LoginController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<string> LoginAsync(string userPhone, string userPassword)
        {
            return await Task.Factory.StartNew(() => Login(userPhone, userPassword));
        }

        public string Login(string userPhone, string userPassword)
        {
            try
            {
                var url = $"https://yiyuan.dabannet.cn/loginNew";
                var content = new LoginContent(userPhone, userPassword);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"登录失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{msg}");
                if ("200".Equals(code) && "OK".Equals(msg))
                {
                    var token = response.JsonBody.RootElement.GetProperty("token").GetString();
                    MainSession.PlatformSession.AddOrUpdate(Constants.Token, token);
                    return token;
                }
                MainSession.SetStatus(MiaoProgress.Init);
                return null;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"登录异常{ex.Message}");
                return null;
            }
        }
    }
}
