using Base.viewmodel.status;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;
using Xihongmen.session;

namespace Xihongmen.login
{
    internal class YzmController : HttpClientBase
    {
        public YzmController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SendYzmAsync(string userPhone)
        {
            Task.Factory.StartNew(() => SendYzm(userPhone));
        }

        public void SendYzm(string userPhone)
        {
            try
            {
                var url = $"https://yiyuan.dabannet.cn/loginNew";
                var content = new YzmContent(userPhone);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"登录失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"SendYzm - {msg}");
                if ("200".Equals(code) && "OK".Equals(msg))
                {
                    var token = response.JsonBody.RootElement.GetProperty("token").GetString();
                    MainSession.PlatformSession.AddOrUpdate(Constants.Token, token);
                    return;
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"登录异常{ex.Message}");
            }
        }
    }
}
