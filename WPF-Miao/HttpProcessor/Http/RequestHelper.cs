using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProcessor.Http
{
    public static class RequestHelper
    {

        #region Get
        public static async Task<string> SearchAsync(this HttpClient client, HttpClientContentBase content)
        {
            var response = await client.GetAsync(content.RequestUrl);
            var resContent = response.Content;
            var resStr = await resContent.ReadAsStringAsync();
            return resStr;
        }

        public static Task<string> Search(this HttpClient client, HttpClientContentBase content)
        {
            return Task.Run(() => client.SearchAsync(content).Result);
        }

        public static void Search(this HttpClient client, HttpClientContentBase content, Action<string> callback)
        {
            var task = client.Search(content);
            callback(task.Result);
        }

        #endregion Get
    }
}
