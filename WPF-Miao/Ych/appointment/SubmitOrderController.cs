using HttpProcessor.Client;
using Ych.common;
using Ych.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

namespace Ych.appointment
{
    internal class SubmitOrderController : HttpClientBase
    {
        public SubmitOrderController(HttpClient httpClient) : base(httpClient)
        {
        }

        public string SubmitOrder(string orderId)
        {
            try
            {
                var url = $"http://101.34.141.250:9653/api/front/appointment/tiJiaoOrder/{orderId}";
                var content = new YchContent(url);
                content.BuildDefaultHeaders(Client);
                HttpDicResponse response = GetStringAsync(content).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"提交Order失败{response?.Message}");
                    return null;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var message = root.GetProperty("message").NotNullString();
                if (!"200".Equals(code) || !message.Contains("成功"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"提交Order失败:code={code}, message={message}");
                    return null;
                }
                var data = root.GetProperty("data").NotNullString();
                if (string.IsNullOrEmpty(data))
                {
                    MainSession.PrintLogEvent.Publish(this, $"提交Order失败:code={code}, message={message}");
                    return null;
                }
                MainSession.PrintLogEvent.Publish(this, $"提交成功:data={data}");
                return data;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"提交Order失败 - {ex.Message} - {ex.StackTrace}");
                return null;
            }
        }
    }
}
