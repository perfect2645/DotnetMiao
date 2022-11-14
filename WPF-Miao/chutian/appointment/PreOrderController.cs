using chutian.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace chutian.appointment
{
    internal class PreOrderController : HttpClientBase
    {
        public PreOrderController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildHeaders(PreOrderContent content)
        {
            BuildClientHeaders(content);
        }

        public void PreOrderAsync(PreOrderContent content)
        {
            Task.Factory.StartNew(() =>
            {
                PreOrder(content);
            });
        }

        public void PreOrder(PreOrderContent content)
        {
            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"没查到苗{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var success = root.GetProperty("success").NotNullString().ToBool();
                var msg = root.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"PreOrder - {msg}");
                if ("0".Equals(code) && success)
                {
                    var miaoInfo = root.GetProperty("obj");
                    AnalysisResult(miaoInfo, content.Order);

                    return;
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void AnalysisResult(JsonElement miaoInfo, Order order)
        {
            if (miaoInfo.ValueKind == JsonValueKind.Null)
            {
                return;
            }

            order.MiaoId = miaoInfo.GetProperty("id").NotNullString();
            MainSession.PrintLogEvent.Publish(this, order.ToLogString());
        }
    }
}
