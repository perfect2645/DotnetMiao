using HttpProcessor.Client;
using Jkgs.common;
using Jkgs.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Jkgs.search
{
    internal class DateController : HttpClientBase
    {
        public DateController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetDateAsync()
        {
            await Task.Factory.StartNew(() => GetDate());
        }

        private void GetDate()
        {
            try
            {
                var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
                var url = $"http://101.34.141.250:9653/api/front/classification/selectDateByYeWuId?type=1&yeWuId={deptId}";
                var content = new JkgsContent(url);
                content.BuildDefaultHeaders(Client);
                HttpDicResponse response = GetStringAsync(content).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"获取日期列表失败{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var message = root.GetProperty("message").NotNullString();
                if (!"200".Equals(code) || !message.Contains("成功"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取日期列表失败:code={code}, message={message}");
                    return;
                }
                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取日期列表失败:code={code}, message={message}");
                    return;
                }
                SaveDateList(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取日期列表失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SaveDateList(JsonElement data)
        {
            var dateRangeElement = data.GetProperty("dates");
            var dateRange = JsonAnalysis.JsonToDicList(dateRangeElement);
            if (!dateRange.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取日期列表失败: userList为空");
                return;
            }
            MainSession.PrintLogEvent.Publish(this, dateRange);

            if (dateRange.Count == 1)
            {

            }

            var startDate = dateRange[0];
            var endDate = dateRange[1];
        }
    }
}
