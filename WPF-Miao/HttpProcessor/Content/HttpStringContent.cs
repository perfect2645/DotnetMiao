using HttpProcessor.ExceptionManager;
using HttpProcessor.JsonFactory;
using System.Text;
using System.Text.Json;
using Utils;

namespace HttpProcessor.Content
{
    public abstract class HttpStringContent
    {
        #region Properties

        public Dictionary<string, string> Headers { get; private set; }
        public Dictionary<string, object> Content { get; private set; }

        public string RequestUrl { get; private set; }

        #endregion Properties

        public HttpStringContent(string url)
        {
            RequestUrl = url;
            Content = new Dictionary<string, object>();
            Headers = new Dictionary<string, string>();
        }

        public virtual StringContent GetJsonContent()
        {
            var jsonContent = JsonSerializer.Serialize(Content, JsonEncoder.JsonOption);
            return new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        #region Header

        public void BuildClientHeaders(HttpClient httpClient)
        {
            try
            {
                foreach (var header in Headers)
                {
                    if (!httpClient.DefaultRequestHeaders.Contains(header.Key))
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new HttpException(ex, "Build HttpClient Header");
            }
        }

        public virtual void BuildDefaultHeaders(HttpClient httpClient)
        {
            BuildClientHeaders(httpClient);
        }

        public void AddHeader(string key, string value)
        {
            Headers.AddOrUpdate(key, value);
        }

        public void AddHeaders(Dictionary<string, string> pairs)
        {
            Headers.AddOrUpdate(pairs);
        }

        #endregion Header

        #region Content

        public void AddContent(string key, object value)
        {
            Content.AddOrUpdate(key, value);
        }

        public void AddContents(Dictionary<string, object> pairs)
        {
            Content.AddOrUpdate(pairs);
        }

        #endregion Content
    }
}
