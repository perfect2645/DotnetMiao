using gaoxin.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace gaoxin.common
{
    internal class GaoxinControllerBase : HttpClientBase
    {
        public Action ProcessAction { get; set; }
        public string Description { get; private set; }

        public GaoxinControllerBase(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task ProcessAsync(GaoxinContent content)
        {
            Description = content.Description;
            await Task.Factory.StartNew(() => Process(content));
        }

        private void Process(GaoxinContent content)
        {
            try
            {
                HttpDicResponse response = GetStringAsync(content).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"{Description}失败{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var result = root.GetProperty("result").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{Description}-{result}");
                if ("Success".Equals(result))
                {
                    var data = root.GetProperty("map");
                    HandleResult(data);
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"{Description}失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        protected virtual void HandleResult(JsonElement data)
        {

        }
    }
}
