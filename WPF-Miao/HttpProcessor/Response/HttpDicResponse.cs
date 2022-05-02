using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HttpProcessor.Client
{
    public class HttpDicResponse
    {
        #region Properties

        public ushort StatusCode { get; private set; }

        public string Message { get; private set; }

        public Dictionary<string, object> Headers { get; private set; }

        public Dictionary<string, object> Body { get; private set; }

        #endregion Properties

        #region Constructor

        public HttpDicResponse(HttpResponseMessage response)
        {
            BuildHeaders(response.Headers);
            BuildBody(response.Content);
        }

        #endregion Constructor

        #region Header

        private void BuildHeaders(HttpResponseHeaders headers)
        {
            var headerMap = headers.GetEnumerator();

            if (headerMap == null)
            {
                return;
            }

            Headers = new Dictionary<string, object>();

            while (headerMap.MoveNext())
            {
                Headers.Add(headerMap.Current.Key, headerMap.Current.Value);
            }
        }

        #endregion Header

        #region Body

        private void BuildBody(HttpContent content)
        {
            var contentStr = content.ReadAsStringAsync().Result;
            var contentDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentStr);
            Body = contentDic ?? new Dictionary<string, object>();
        }

        #endregion Body

        #region Status

        public virtual void SetStatus(ushort status)
        {
            StatusCode = status;
        }

        public virtual void SetMessage(string msg)
        {
            Message = msg;
        }

        private void SetStatus()
        {

        }

        #endregion Status
    }
}
