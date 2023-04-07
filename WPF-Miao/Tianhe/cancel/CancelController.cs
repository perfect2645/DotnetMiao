using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tianhe.login;
using Tianhe.session;
using Utils.stringBuilder;

namespace Tianhe.cancel
{
    internal class CancelController : HttpClientBase
    {
        public CancelController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void CancelAsync(TianheLogin user, string cancelId)
        {
            Task.Factory.StartNew(() =>
            {
                Cancel(user, cancelId);
            });
        }

        private void Cancel(TianheLogin user, string cancelId)
        {
            try
            {
                var content = new CancelContent(user, cancelId);
                content.BuildDefaultHeaders(Client);

                HttpDicResponse response = PostStringAsync(content, ContentType.String, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"取消预约失败 - cancelId:{cancelId}, {response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                JsonElement error;
                var isError = root.TryGetProperty("error", out error);
                if (isError)
                {
                    var errMsg = error.NotNullString();
                    MainSession.PrintLogEvent.Publish(this, $"取消预约失败:cancelId:{cancelId}, error {errMsg}");
                    return;
                }
                var msg = root.GetProperty("message").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"取消预约结果:cancelId:{cancelId}, message: {msg}");
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"取消异常cancelId:{cancelId}, {ex.Message}");
            }
        }
    }
}
