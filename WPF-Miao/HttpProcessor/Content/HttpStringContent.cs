using HttpProcessor.JsonFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;

namespace HttpProcessor.Content
{
    public abstract class HttpStringContent
    {
        public Dictionary<string, object> Content { get; private set; }
        public string RequestUrl { get; private set; }

        public HttpStringContent(string url)
        {
            RequestUrl = url;
            Content = new Dictionary<string, object>();
        }

        public virtual StringContent GetJsonContent()
        {
            var jsonContent = JsonSerializer.Serialize(Content, JsonEncoder.JsonOption);

            return new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        #region Add or Update

        public void Add(string key, object value)
        {
            Content.AddOrUpdate(key, value);
        }

        public void Add(Dictionary<string, object> pairs)
        {
            Content.AddOrUpdate(pairs);
        }

        #endregion Add or Update
    }
}
