using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Shengzhi.common;
using Shengzhi.login;
using Shengzhi.session;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.stringBuilder;

namespace Shengzhi.search
{
    internal class DateController : HttpClientBase
    {
        public DateController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool GetDate(ShengzhiLogin user)
        {
            try
            {
                var content = new DateContent(user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetDate - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var detail = root.GetProperty("detail");
                if (detail.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取日期信息失败: results is empty");
                    return false;
                }
                return CheckDate(detail, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取日期信息异常{ex.Message}");
                return false;
            }
        }

        private bool CheckDate(JsonElement data, ShengzhiLogin user)
        {
            var dates = JsonAnalysis.JsonToDicList(data);
            if (!dates.HasItem())
            {
                Log($"获取日期信息失败");
                return false;
            }

            var validDates = dates.Where(d => DateTimeUtil.IsEqualOrGreaterThanToday(d["date"].NotNullString()));
            if (!validDates.HasItem())
            {
                Log("没有可用日期");
                return false;
            }

            var dateList = validDates.Select(d => d["date"].NotNullString()).ToList();
            var today = DateTimeUtil.GetToday();
            dateList = dateList.Where(d => today != d).ToList();
            if (!dateList.HasItem())
            {
                Log("没有可用日期");
                return false;
            }

            MainSession.PlatformSession.AddOrUpdate("orderDates", dateList);

            MainSession.PrintLogEvent.Publish(this, "获得预约日期");
            return true;
        }
    }
}
