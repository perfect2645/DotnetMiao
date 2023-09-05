using HttpProcessor.Client;
using B114.common;
using B114.login;
using B114.session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace B114.cancel
{
    internal class SearchSuccessController : HttpClientBase
    {
        public SearchSuccessController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task SearchHistoryAsync(B114Login user)
        {
            return Task.Factory.StartNew(() => SearchHistory(user));
        }

        private void SearchHistory(B114Login user)
        {
            try
            {
                var content = new B114Content("", user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchHistory - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var results = root.GetProperty("results");
                if (results.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取预约历史失败: results is empty");
                    return;
                }
                SaveHistory(results, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取预约历史异常{ex.Message}");
            }
        }

        private void SaveHistory(JsonElement data, B114Login user)
        {
            var histories = JsonAnalysis.JsonToDicList(data);
            if (!histories.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取预约历史失败");
                return;
            }

            var validHistories = histories.Where(h => h["get_status_display"].NotNullString() == "预约成功"
            || h["get_status_display"].NotNullString() == "待支付").ToList();

            var historyList = new List<History>();
            foreach (var his in validHistories)
            {
                var date = his.GetString("see_date");
                var time = his.GetString("see_start_time");
                var history = new History
                {
                    id = his.GetString("id"),
                    name = his.GetString("name"),
                    see_date = date,
                    see_start_time = time,
                    see_end_time = his.GetString("see_end_time"),
                    Key = $"{date} | {time}",
                };

                historyList.Add(history);
            }

            MainSession.PlatformSession["history"] = historyList;
        }
    }
}
