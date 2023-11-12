using Gzjk.appointment;
using Gzjk.session;
using HttpProcessor.Client;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;

namespace Gzjk.search
{
    internal class DateController : HttpClientBase
    {
        public string Date { get; set; }
        public DateController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchDate()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new DateContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDate - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var status = root.GetProperty("status").GetInt32();
                if (status != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDate失败: status={status}");
                    return false;
                }

                var list = root.GetProperty("list");
                if (list.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDate失败: list is empty");
                    return false;
                }

                return CheckSaveVaccine(list);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到date - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveVaccine(JsonElement datetListData)
        {
            var dateList = JsonAnalysis.JsonToDicList(datetListData);

            if (!dateList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"还没开始, DateList is empty");
                return false;
            }

            var validDateList = dateList.Where(d => d.GetString("enable").ToBool()).ToList();
            if (!validDateList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"还没开始, DateList is empty");
                return false;
            }

            var defaultDateItem = validDateList.LastOrDefault();

            Date = defaultDateItem.GetString("date");
            MainSession.PrintLogEvent.Publish(this, $"{validDateList.Count}个日期可约, 默认选择了{Date}");

            MainSession.SetStatus(Base.viewmodel.status.MiaoProgress.MiaoGet);

            return true;
        }
    }
}