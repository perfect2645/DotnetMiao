using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using HttpProcessor.Request;
using HttpProcessor.Response;
using Logging;

namespace HttpProcessor.Client
{
    public class HttpClientBase : IHttpClient
    {
        protected readonly HttpClient Client;

        private static readonly string _uuid;

        static HttpClientBase()
        {
            _uuid = Guid.NewGuid().ToString();
            GLog.Logger.Info($"HttpClient Created uuid={_uuid}");
        }

        public HttpClientBase(HttpClient httpClient)
        {
            Client = httpClient;
        }

        #region Search

        public virtual async Task<HttpDicResponse> SearchAsync(HttpMessageContent content)
        {
            try
            {
                var response = await Client.SearchAsync(content);
                return response;
            } 
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return null;
            }
        }

        public virtual async Task<HttpDicResponse> SearchAsync(string url)
        {
            try
            {
                var response = await Client.SearchAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return null;
            }
        }

        public async Task<Stream> SearchStreamAsync(string url)
        {
            try
            {
                var response = await Client.GetStreamAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return null;
            }
        }

        public async Task<byte[]> SearchByteAsync(string url)
        {
            try
            {
                var response = await Client.GetByteArrayAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return null;
            }
        }

        public virtual void Search(HttpMessageContent content, Action<HttpDicResponse> callback)
        {
            try
            {
                Client.Search(content, callback);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
            }
        }

        public virtual async Task<HtmlResponse> SearchHtml(string url)
        {
            var response = await Client.SearchHtml(url);
            return response;
        }

        #endregion Search

        #region Get

        public virtual async Task<string> GetStringAsync(HttpStringContent content)
        {
            try
            {
                return await Client.GetStringAsync(content.RequestUrl);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("GetStringAsync Failed", ex);
                return null;
            }
        }

        public virtual async Task<Stream> GetStreamAsync(HttpStringContent content)
        {
            try
            {
                return await Client.GetStreamAsync(content.RequestUrl);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("GetStringAsync Failed", ex);
                return null;
            }
        }

        #endregion Get

        #region Post String

        public virtual async Task<HttpDicResponse> PostStringAsync(HttpStringContent content, ContentType contentType = ContentType.Json)
        {
            try
            {
                if (contentType == ContentType.String)
                {
                    return await Client.PostStringAsync(content);
                }
                if (contentType == ContentType.Encode)
                {
                    return await Client.PostRichEncodeAsync(content);
                }
                return await Client.PostJsonAsync(content);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return null;
            }
        }

        #endregion Post String
    }
}
