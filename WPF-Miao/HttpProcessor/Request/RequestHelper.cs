using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using HttpProcessor.Response;
using Logging;
using System.Net.Http.Headers;

namespace HttpProcessor.Request
{
    public static class RequestHelper
    {
        #region Get
        public static async Task<HttpDicResponse> SearchAsync(this HttpClient client, HttpMessageContent content)
        {
            if (content.HttpRequestMessage?.Headers != null)
            {
                return await SendAsync(client, content);
            }

            return await GetAsync(client, content);
        }

        private static async Task<HttpDicResponse> GetAsync(HttpClient client, HttpMessageContent content)
        {
            HttpDicResponse? dicResponse = null;

            try
            {
                var response = await client.GetAsync(content.RequestUrl);
                response.EnsureSuccessStatusCode();
                dicResponse = new HttpDicResponse(response);
            }
            catch (HttpException httpEx)
            {
                GLog.Logger.Error($"StatusCode:{httpEx.ErrCode}", httpEx);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
            }

            return dicResponse;
        }

        private static async Task<HttpDicResponse> SendAsync(HttpClient client, HttpMessageContent content)
        {
            try
            {
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                var response = await client.SendAsync(content.HttpRequestMessage);
                response.EnsureSuccessStatusCode();
                return new HttpDicResponse(response);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
                throw new HttpException("SendAsync");
            }
        }

        public static async Task<HttpDicResponse> SearchAsync(this HttpClient client, string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return new HttpDicResponse(response);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
                throw new HttpException("SearchAsync");
            }
        }

        public static async Task<HtmlResponse> SearchHtml(this HttpClient client, string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return new HtmlResponse(response);
            }
            catch (HttpException ex)
            {
                GLog.Logger.Error(ex);
                throw new HttpException(ex, ex.ErrCode ?? "SearchHtml error");
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
                return null;
            }
        }

        public static async Task<HttpDicResponse> GetJsonAsync(this HttpClient client, HttpStringContent content)
        {
            try
            {
                var response = await client.GetAsync(content.RequestUrl);
                response.EnsureSuccessStatusCode();
                return new HttpDicResponse(response);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
                throw new HttpException(ex, "GetJsonAsync");
            }
        }

        #endregion Get

        #region Post

        public static async Task<HttpDicResponse> PostJsonAsync(this HttpClient client, HttpStringContent content, bool ensureSuccess = true)
        {
            try
            {
                var response = await client.PostAsync(content.RequestUrl, content.GetJsonContent());
                if (ensureSuccess)
                {
                    response.EnsureSuccessStatusCode();
                }
                return new HttpDicResponse(response);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
                throw new HttpException(ex, "PostJsonAsync");
            }
        }

        public static async Task<HttpDicResponse> PostStringAsync(this HttpClient client, HttpStringContent content, bool ensureSuccess = true)
        {
            try
            {
                var strContent = content.GetStringContent();
                var response = await client.PostAsync(content.RequestUrl, strContent);
                if (ensureSuccess)
                {
                    response.EnsureSuccessStatusCode();
                }
                return new HttpDicResponse(response);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
                throw new HttpException(ex.Message, "PostStringAsync");
            }
        }

        public static async Task<HttpDicResponse> PostRichEncodeAsync(this HttpClient client, HttpStringContent content)
        {
            try
            {
                var strContent = content.GetRichStringContent();
                var response = await client.PostAsync(content.RequestUrl, strContent);
                response.EnsureSuccessStatusCode();
                return new HttpDicResponse(response);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
                throw new HttpException(ex, "PostRichEncodeAsync");
            }
        }

        #endregion Post
    }
}
