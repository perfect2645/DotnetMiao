using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using HttpProcessor.JsonFactory;
using Utils;
using WPF_Miao.Platform.yunnan.session;

namespace WPF_Miao.Platform.yunnan.model
{
    internal class HeaderTimestamp
    {
        public string Uuid { get; private set; }
        public string Timestamp { get; private set; }
        public string XCaNonce { get; private set; }
        public HttpDicResponse SearchResponse { get; }

        public HeaderTimestamp(HttpDicResponse searchResponse)
        {
            SearchResponse = searchResponse;
            Build();
        }

        private void Build()
        {
            var jsonElement = SearchResponse.JsonBody.RootElement;
            if (jsonElement.GetStringValue("code") != "200")
            {
                throw new HttpException($"yunan --- Get HeaderTimestamp returns {SearchResponse.Body["code"]}");
            }
            Timestamp = jsonElement.GetStringValue("body");
            Uuid = jsonElement.GetStringValue("uuid");
            XCaNonce = $"{Uuid}{Timestamp}{AppSession.LocalTimeOffset}";
        }
    }
}
