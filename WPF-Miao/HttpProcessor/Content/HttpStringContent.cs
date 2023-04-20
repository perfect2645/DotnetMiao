using HttpProcessor.ExceptionManager;
using System.Text;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace HttpProcessor.Content
{
    public abstract class HttpStringContent
    {
        #region Properties

        public Dictionary<string, string> Headers { get; private set; }
        public Dictionary<string, object> Content { get; private set; }
        public string ContentType { get; set; } = "application/json";
        public string RequestUrl { get; set; }
        public string ContentPrefix { get; set; }
        public string ContentSuffix { get; set; }
        public bool IsEncode { get; set; }

        #endregion Properties

        public HttpStringContent(string url)
        {
            RequestUrl = url;
            Content = new Dictionary<string, object>();
            Headers = new Dictionary<string, string>();
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
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                    else
                    {
                        httpClient.DefaultRequestHeaders.Remove(header.Key);
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
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

        public void AddHeader(Dictionary<string, object> source, string key)
        {
            Headers.AddOrUpdate(key, source[key].NotNullString());
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

        public void AddEncodeContent(string key, object value)
        {
            Content.AddOrUpdate(key, UnicodeConverter.EncodeOriginal(value.NotNullString(), true));
        }

        public void AddContent(Dictionary<string, object> source, string key)
        {
            Content.AddOrUpdate(key, source[key]);
        }

        public void AddContents(Dictionary<string, object> pairs)
        {
            Content.AddOrUpdate(pairs);
        }

        public virtual StringContent GetJsonContent()
        {
            var jsonContent = JsonSerializer.Serialize(Content, JsonEncoder.JsonOption);
            if (IsEncode)
            {
                jsonContent = UnicodeConverter.Encode(jsonContent, true);
            }
            if (!string.IsNullOrEmpty(ContentPrefix))
            {
                jsonContent = $"{ContentPrefix}{jsonContent}";
            }
            if (!string.IsNullOrEmpty(ContentSuffix))
            {
                jsonContent = $"{jsonContent}{ContentSuffix}";
            }

            return new StringContent(jsonContent, Encoding.UTF8, ContentType);
        }

        public virtual StringContent GetStringContent()
        {
            var sb = new StringBuilder();
            foreach(var item in Content)
            {
                sb.Append($"{item.Key}={item.Value}&");
            }
            var stringContent = sb.ToString().TrimEnd('&');

            return new StringContent(stringContent, Encoding.UTF8, ContentType);
        }

        public virtual StringContent GetRichStringContent()
        {
            var sb = new StringBuilder();

            foreach (var item in Content)
            {
                var valueJson = ToJson(item.Value);
                var encodeValue = UnicodeConverter.Encode(valueJson, true);
                sb.Append($"{item.Key}={encodeValue}&");
            }
            var stringContent = sb.ToString().TrimEnd('&');

            return new StringContent(stringContent, Encoding.UTF8, ContentType);
        }

        private string ToJson(object content)
        {
            var json = JsonSerializer.Serialize(content, JsonEncoder.JsonOption);

            return json;
        }

        #endregion Content
    }
}
