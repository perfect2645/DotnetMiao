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

        public async Task<string> LoginAsync(string userPhone, string yzm)
        {
            return await Task.Factory.StartNew(() => Login(userPhone, yzm));
        }

        public string Login(string userPhone, string yzm)
        {
            try
            {
                var url = $"https://yiyuan.dabannet.cn/loginNew";
                var content = new LoginContent(userPhone, yzm);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"登录失败 - {response?.Message},请检查参数");
                    return null;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                
                if ("200".Equals(code) && "OK".Equals(msg))
                {
                    var token = root.GetProperty("data").GetProperty("token").GetString();
                    MainSession.PlatformSession.AddOrUpdate(Constants.Token, token);
                    MainSession.PrintLogEvent.Publish(this, $"登录成功 - token:{token}");
                    return token;
                }

                MainSession.PrintLogEvent.Publish(this, $"{msg}");
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
