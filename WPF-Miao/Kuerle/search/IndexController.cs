using HttpProcessor.Client;
using Kuerle.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;

namespace Kuerle.search
{
    public class IndexController : HttpClientBase
    {
        public IndexController(HttpClient httpClient) : base(httpClient)
        {
        }

        public (string, string) GetIndex()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new IndexContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetIndex - {response?.Message},请检查参数");
                    return (string.Empty, string.Empty);
                }
                var bookInfoStr = response.ContentStr;

                return SaveIndex(bookInfoStr);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetIndex - {ex.Message} - {ex.StackTrace}");
                return (string.Empty, string.Empty);
            }
        }

        private (string, string) SaveIndex(string bookInfoStr)
        {
            if (string.IsNullOrEmpty(bookInfoStr))
            {
                MainSession.PrintLogEvent.Publish(this, "bookInfo is empty");
                return (string.Empty, string.Empty);
            }

            var pidKeyIndex = bookInfoStr.LastIndexOf("\"GUID\\\":\\\"");
            if (pidKeyIndex == -1)
            {
                MainSession.PrintLogEvent.Publish(this, "pidKeyIndex is not found");
                return (string.Empty, string.Empty);
            }
            var pidKey = bookInfoStr.Substring(pidKeyIndex + 10, 36);
            if (string.IsNullOrEmpty(pidKey))
            {
                MainSession.PrintLogEvent.Publish(this, "pidKeyIndex is not correct");
                return (string.Empty, string.Empty);
            }

            MainSession.PrintLogEvent.Publish(this, $"pidKey = {pidKey}");
            MainSession.PlatformSession.AddOrUpdate(Constants.PidKey, pidKey);

            var vidKeyIndex = bookInfoStr.LastIndexOf("CLIENTODATA(");
            if (vidKeyIndex == -1)
            {
                MainSession.PrintLogEvent.Publish(this, "vidKeyIndex is not found");
                return (pidKey, string.Empty);
            }
            var vidKey = bookInfoStr.Substring(vidKeyIndex + 16, 36);
            if (string.IsNullOrEmpty(vidKey))
            {
                MainSession.PrintLogEvent.Publish(this, "vidKeyIndex is not correct");
                return (pidKey, string.Empty);
            }

            MainSession.PrintLogEvent.Publish(this, $"vidKey = {vidKey}");
            MainSession.PlatformSession.AddOrUpdate(Constants.VidKey, vidKey);

            return (pidKey, vidKey);
        }
    }
}
