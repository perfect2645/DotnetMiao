using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using HttpProcessor.Request;
using HttpProcessor.Response;
using Logging;
using System.Net.Http.Headers;

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

        #region Header

        public void BuildClientHeaders(HttpStringContent content)
        {
            try
            {
                foreach (var header in content.Headers)
                {
                    if (!Client.DefaultRequestHeaders.Contains(header.Key))
                    {
                        Client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                    else
                    {
                        Client.DefaultRequestHeaders.Remove(header.Key);
                        Client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(ex, "Build HttpClient Header");
            }
        }

        public void AddHeaderByName(string name)
        {
            Client.DefaultRequestHeaders.UserAgent.TryParseAdd(name);
        }

        #endregion Header

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

        public virtual async Task<HtmlResponse> SearchHtml(string url)
        {
            var response = await Client.SearchHtml(url);
            return response;
        }

        #endregion Search

        #region Get

        public virtual async Task<HttpDicResponse> GetStringAsync(HttpStringContent content)
        {
            try
            {
                return await Client.GetJsonAsync(content);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("GetStringAsync", ex);
                return new HttpDicResponse(ex.Message);
            }
        }

        public virtual async Task<HttpDicResponse> GetImageAsync(HttpStringContent content)
        {
            try
            {
                var response = await Client.GetAsync(content.RequestUrl);
                return new HttpDicResponse(response);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
                throw new HttpException(ex, "GetImageAsync");
            }
        }

        #endregion Get

        #region Post String

        public virtual async Task<HttpDicResponse> PostStringAsync(HttpStringContent content, 
            ContentType contentType = ContentType.Json, 
            bool ensureSuccess = true)
        {
            try
            {
                if (contentType == ContentType.String)
                {
                    return await Client.PostStringAsync(content, ensureSuccess);
                }
                if (contentType == ContentType.Encode)
                {
                    return await Client.PostRichEncodeAsync(content);
                }
                if (contentType == ContentType.Array)
                {
                    return await Client.PostArrayAsync(content, false);
                }
                return await Client.PostJsonAsync(content, ensureSuccess);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return new HttpDicResponse(ex.Message);
            }
        }

        public virtual async Task<HttpDicResponse> PostStringDecodeAsync(HttpStringContent content,
            Func<string, string> decodeAction,
            ContentType contentType = ContentType.Json)
        {
            try
            {
                var response = await Client.PostAsync(content.RequestUrl, content.GetJsonContent());
                response.EnsureSuccessStatusCode();
                return new HttpDicResponse(response, decodeAction);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return new HttpDicResponse(ex.Message);
            }
        }

        #endregion Post String

        #region Log

        public void Log(string message)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Logging.GLog.Logger.Info($"[Thread: {threadId}]{message}");
        }

        #endregion Log
    }
}
