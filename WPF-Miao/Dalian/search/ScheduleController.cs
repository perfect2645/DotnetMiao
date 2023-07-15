using Base.viewmodel.status;
using Dalian.appointment;
using Dalian.session;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;
using Dalian.common;

namespace Dalian.search
{
    internal class ScheduleController : DalianController
    {
        public ScheduleController(HttpClient httpClient) : base(httpClient)
        {
        }

        public (bool, List<string>) SearchSchedule()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new ScheduleContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDates - {response?.Message},请检查参数");
                    return (false, new List<string>());
                }

                var root = response.JsonBody.RootElement;

                var status = root.GetProperty("status").GetInt32();
                var msg = root.GetProperty("msg").GetString();
                if (status != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: status={status}, msg={msg}");
                    return (false, new List<string>());
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: results is empty");
                    return (false, new List<string>());
                }

                var dateInfo = data.GetProperty("regDateInfos");

                return CheckSaveDates(dateInfo);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return (false, new List<string>());
            }
        }

        private (bool, List<string>) CheckSaveDates(JsonElement scheduleInfoData)
        {
            var dateInfoList = JsonAnalysis.JsonToDicList(scheduleInfoData);
            if (!dateInfoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗");
            }

            var dateList = new List<string>();
            var avaliableDateList = dateInfoList.Where(x => x.GetString("scheduleStatus") == "1").ToList();
            if (!avaliableDateList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return (false, new List<string>());
            }

            foreach (var dateInfo in avaliableDateList)
            {
                dateList.Add(dateInfo.GetString("scheduleDate"));
            }

            MainSession.SetStatus(MiaoProgress.MiaoGet);

            return (true, dateList);
        }
    }
}
