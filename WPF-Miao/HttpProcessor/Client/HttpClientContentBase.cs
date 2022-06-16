using HttpProcessor.JsonFactory;
using System.Text;
using System.Text.Json;
using Utils;

namespace HttpProcessor.Client
{
    public abstract class HttpClientContentBase
    {
        private readonly Dictionary<string, object> _contents = new Dictionary<string, object>();

        public Dictionary<string, object> Contents
        {
            get { return _contents; }
        }

        public string RequestUrl { get; private set; }

        public HttpRequestMessage HttpRequestMessage { get; private set; }

        public HttpClientContentBase()
        {
            HttpRequestMessage = new HttpRequestMessage();
        }

        public HttpClientContentBase(string requestUrl)
        {
            RequestUrl = requestUrl;
        }

        #region Headers

        public void AddHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                HttpRequestMessage.Headers.Add(header.Key, header.Value);
            }
        }

        internal void AddHeader(string key, string value)
        {
            HttpRequestMessage.Headers.Add(key, value);
        }

        #endregion Headers

        #region Contents

        internal void AddContents(Dictionary<string, object> contents)
        {
            Contents.AddOrUpdate(contents);
        }

        internal void AddContent(string key, string value)
        {
            Contents.AddOrUpdate(key, value);
        }


        #endregion Contents

        public StringContent GetJsonContent()
        {
            var jsonString = JsonSerializer.Serialize(_contents, JsonEncoder.JsonOption);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
