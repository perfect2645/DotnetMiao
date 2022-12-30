using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;
using Xihongmen.login;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync()
        {
            Task.Factory.StartNew(() => GetUser());
        }

        public void GetUser()
        {
            try
            {
                var url = $"https://yiyuan.dabannet.cn/loginNew";
                var content = new UserContent();
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"GetUser - {msg}");
                if ("200".Equals(code) && "OK".Equals(msg))
                {
                    var token = response.JsonBody.RootElement.GetProperty("token").GetString();
                    MainSession.PlatformSession.AddOrUpdate(Constants.Token, token);
                    return;
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户异常{ex.Message}");
            }
        }
    }
}
