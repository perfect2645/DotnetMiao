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
    internal class DateController : HttpClientBase
    {
        public DateController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool GetDateAndTime(HuangshiLogin user, string date)
        {
            try
            {
                var content = new DateContent(user, date);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null && string.IsNullOrEmpty(response?.ContentStr))
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetDate - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                MainSession.PrintLogEvent.Publish(this, $"获取日期信息失败: results is empty");
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

            var validDates = dates.Where(d => DateTimeUtil.IsEqualOrGreaterThanToday(d["date"].NotNullString()));
            if (!validDates.HasItem())
            {
                Log("没有可用日期");
                return false;
            }

            var dateList = validDates.Select(d => d["date"].NotNullString()).ToList();
            var today = DateTimeUtil.GetToday();
            dateList = dateList.Where(d => today != d).ToList();

            MainSession.PlatformSession.AddOrUpdate("orderDates", dateList);

            MainSession.PrintLogEvent.Publish(this, "获得预约日期");
            return true;
        }
    }
}
