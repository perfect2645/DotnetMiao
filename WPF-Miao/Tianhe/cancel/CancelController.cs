using HttpProcessor.Client;
using HttpProcessor.Content;
using System.Net.Http;
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

        public void CancelAsync(TianheLogin user)
        {
            Task.Factory.StartNew(() =>
            {
                Cancel(user);
            });
        }

        private void Cancel(TianheLogin user)
        {
            var content = new CancelContent(user);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String, false).Result;
            if (response?.Body == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"取消预约失败 - {response?.Message}");
                return;
            }

            var root = response.JsonBody.RootElement;
            var msg = root.GetProperty("message").NotNullString();
            if (msg != "取消成功")
            {
                MainSession.PrintLogEvent.Publish(this, $"取消预约失败:message: {msg}");
            }
        }
    }
}
