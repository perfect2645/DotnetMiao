using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Huangshi.common;
using Huangshi.login;
using Huangshi.session;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.stringBuilder;

namespace Huangshi.search
{
    internal class TimeController : HttpClientBase
    {
        public TimeController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool GetDateAndTime(HuangshiLogin user, string date)
        {
            try
            {
                var content = new TimeContent(user, date);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null && string.IsNullOrEmpty(response?.ContentStr))
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetDate - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;
                return SaveDateTime(root, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取日期信息异常{ex.Message}");
                return false;
            }
        }

        private bool SaveDateTime(JsonElement data, HuangshiLogin user)
        {
            var dates = JsonAnalysis.JsonToDicList(data);
            if (!dates.HasItem())
            {
                Log($"获取日期信息失败");
                return false;
            }

            
            return true;
        }
    }
}
