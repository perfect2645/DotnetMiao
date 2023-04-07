using HttpProcessor.Client;
using Longchi.session;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Longchi.search
{
    internal class TimeController : HttpClientBase
    {
        public TimeController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetDateAsync(string dizhi, string cookie)
        {
            Task.Factory.StartNew(() => GetDate(dizhi, cookie));
        }

        private void GetDate(string dizhi, string cookie)
        {
            var content = new TimeContent(dizhi, cookie);
            content.BuildDefaultHeaders(Client);
            var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
            if (response?.Body == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                return;
            }
            var root = response.JsonBody.RootElement;
            var code = root.GetProperty("code").NotNullString();
            var msg = root.GetProperty("msg").NotNullString();
            MainSession.PrintLogEvent.Publish(this, $"获取时间信息msg:{msg}");
            if (code != "1")
            {
                MainSession.PrintLogEvent.Publish(this, $"获取时间信息失败:{msg}");
                return;
            }
            var data = root.GetProperty("rows");
            SaveTime(data);
        }

        private void SaveTime(JsonElement data)
        {
            var dateList = JsonAnalysis.JsonToDicList(data);
            if (!dateList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取时间信息失败");
                return;
            }

            foreach(var dateInfo in dateList)
            {
                var beginTime = dateInfo.GetString("begin");
            }


            //MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
