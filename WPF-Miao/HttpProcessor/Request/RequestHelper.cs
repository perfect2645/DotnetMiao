using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using Logging;
using System.Net.Http.Headers;

namespace HttpProcessor.Request
{
    public static class RequestHelper
    {
        #region Get
        public static async Task<HttpDicResponse> SearchAsync(this HttpClient client, HttpClientContentBase content)
        {
            if (content.HttpRequestMessage?.Headers != null)
            {
                return await SendAsync(client, content);
            }

            return await GetAsync(client, content);
        }

        private static async Task<HttpDicResponse> GetAsync(HttpClient client, HttpClientContentBase content)
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
                GLog.Logger.Error($"StatusCode:{httpEx.StatusCode}", httpEx);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error(ex);
            }

            return dicResponse;
        }

        private static async Task<HttpDicResponse> SendAsync(HttpClient client, HttpClientContentBase content)
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

        public static Task<HttpDicResponse> Search(this HttpClient client, HttpClientContentBase content)
        {
            return Task.Run(() => client.SearchAsync(content).Result);
        }

        public static void Search(this HttpClient client, HttpClientContentBase content, Action<HttpDicResponse> callback)
        {
            var task = client.Search(content);
            callback(task.Result);
        }

        #endregion Get
    }
}
