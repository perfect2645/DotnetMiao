using HttpProcessor.Client;
using SixWater.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace SixWater.search
{
    internal class ScheduleController : HttpClientBase
    {
        public string Date { get; private set; }

        public ScheduleController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchScheduleAsync(string date)
        {
            Task.Factory.StartNew(() => SearchSchedule(date));
        }

        public (bool, string) SearchSchedule(string date)
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new ScheduleContent(defaultUser, date);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchSchedule - {response?.Message},请检查参数");
                    return (false, string.Empty);
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchSchedule: code={code}, message={msg}");
                    return (false, string.Empty);
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchSchedule失败: data is empty");
                    return (false, string.Empty);
                }

                return CheckSaveSchedule(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return (false, string.Empty);
            }
        }

        private (bool, string) CheckSaveSchedule(JsonElement dataElement)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(dataElement);
            if (!scheduleList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Schedule信息失败");
                return (false, string.Empty);
            }

            var docName = MainSession.PlatformSession.GetString(Constants.DoctorName);
            var targetSchedule = scheduleList.FirstOrDefault(s => s.GetString("doctorName").Contains(docName));
            if (targetSchedule == null)
            {
                targetSchedule = scheduleList.FirstOrDefault(s => s.GetString("doctorName").Contains("九价"));
            }

            if (targetSchedule == null)
            {
                targetSchedule = scheduleList.FirstOrDefault(s => s.GetString("doctorName").Contains("9"));
            }

            if (targetSchedule == null)
            {
                targetSchedule = scheduleList.LastOrDefault();
            }

            var targetScheduleId = targetSchedule.GetString("doctorScheduleId");

            return (true, targetScheduleId);
        }
    }
}
