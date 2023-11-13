using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using YzmSender.session;

namespace YzmSender.search
{
    internal class SendYzmController : HttpClientBase
    {

        public SendYzmController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SendYzmAsync()
        {
            Task.Factory.StartNew(() => SendYzm());
        }

        public bool SendYzm()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new SendYzmContent();
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SendYzm - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetString();
                var success = root.GetProperty("success").GetBoolean();
                var msg = root.GetProperty("msg").GetBoolean();
                if (code != "200" || !success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SendYzm失败: code={code}, msg={msg}");
                    return false;
                }

                MainSession.PrintLogEvent.Publish(this, $"SendYzm成功: code={code}, msg={msg}");
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }
    }
}
