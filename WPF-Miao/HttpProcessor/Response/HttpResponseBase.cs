using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpProcessor.Response
{
    public abstract class HttpResponseBase
    {
        public Dictionary<string, object> Headers { get; private set; }

        #region Header

        protected void BuildHeaders(HttpResponseHeaders headers)
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

        protected abstract void BuildBody(HttpContent content);

        #endregion Body
    }
}
