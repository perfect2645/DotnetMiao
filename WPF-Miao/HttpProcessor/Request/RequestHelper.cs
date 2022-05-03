using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProcessor.Request
{
    public static class RequestHelper
    {

        #region Get
        public static async Task<HttpDicResponse> SearchAsync(this HttpClient client, HttpClientContentBase content)
        {
            var response = await client.GetAsync(content.RequestUrl);
            var dicResponse = new HttpDicResponse(response);
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
