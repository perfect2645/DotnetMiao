using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using Logging;

namespace HttpProcessor.Request
{
    public static class RequestHelper
    {
        #region Get
        public static async Task<HttpDicResponse> SearchAsync(this HttpClient client, HttpClientContentBase content)
        {
            HttpDicResponse? dicResponse = null;

            try
            {
                var response = await client.GetAsync(content.RequestUrl);
                response.EnsureSuccessStatusCode();
                dicResponse = new HttpDicResponse(response);
            }
            catch(HttpException httpEx)
            {
                GLog.Logger.Error($"StatusCode:{httpEx.StatusCode}", httpEx);
            }
            catch(Exception ex)
            {
                GLog.Logger.Error(ex);
            }

            return dicResponse;
        }

        public static async Task<HttpDicResponse> SearchAsync(this HttpClient client, string url)
        {
            HttpDicResponse? dicResponse = null;

            try
            {
                var response = await client.GetAsync(url);
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
