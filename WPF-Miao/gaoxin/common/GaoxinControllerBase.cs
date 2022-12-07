using gaoxin.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

namespace gaoxin.common
{
    internal class GaoxinControllerBase : HttpClientBase
    {
        public Action<GaoxinContent> ProcessAction { get; set; }
        public Action<Task<GaoxinContent>> ProcessAsyncAction { get; set; }
        public string Description { get; private set; }

        public GaoxinControllerBase(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task ProcessAsyncTask(GaoxinContent content)
        {
            Description = content.Description;
            await Task.Factory.StartNew(() => Process(content));
        }

        public async void ProcessAsync(GaoxinContent content)
        {
            Description = content.Description;
            await Task.Factory.StartNew(() => Process(content));
        }

        private void Process(GaoxinContent content)
        {
            try
            {
                Task.Run(() => ProcessAction?.Invoke(content));
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"{Description}失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        protected JsonElement CheckGetResultValue(HttpDicResponse response)
        {
            var nullElement = new JsonElement();
            if (response?.JsonBody?.RootElement == null)
            {
                Log($"{Description}失败{response?.Message}");
                return nullElement;
            }

            var root = response.JsonBody.RootElement;
            var code = root.GetProperty("code").NotNullString();
            if (code != "1")
            {
                JsonElement msg;
                root.TryGetProperty("msg", out msg);
                if (msg.ValueKind != JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(null, msg.NotNullString());
                    return msg;
                }
            }
            var result = root.GetProperty("value");
            return result;
        }

        protected virtual void HandleResult(JsonElement data)
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
        }

        protected string Decode(string content)
        {
            var result = Encryptor.DeCodePkcs7(content);

            return result;
        }
    }
}
