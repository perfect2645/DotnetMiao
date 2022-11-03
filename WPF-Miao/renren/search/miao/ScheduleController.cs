using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using renren.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace renren.search.miao
{
    internal class ScheduleController : HttpClientBase
    {
        private readonly object ScheduleLock = new object();

        public ScheduleController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetServiceScheduleAsync()
        {
            try
            {
                if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.GettingMiao)
                {
                    var schedules = await Task.Factory.StartNew(() => GetServiceSchedule());
                    if (!schedules.HasItem())
                    {
                        MainSession.PrintLogEvent.Publish(this, $"没查到苗！schedule is empty");
                        return;
                    }
                }

                lock(ScheduleLock)
                {
                    if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.MiaoGet)
                    {
                        lock (ScheduleLock)
                        {
                            var detailController = HttpServiceController.GetService<ScheduleDetailController>();
                            var schedules = MainSession.MiaoSession["Schedule"] as List<Schedule>;
                            detailController.BulkGetDetailAsync(schedules);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
            }
        }

        public List<Schedule> GetServiceSchedule()
        {
            var url = "https://www.medic.ren/PM-server/mobserviceTimeDef/getServiceSchedule";

            var content = new ScheduleContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetServiceSchedule - response == null");
                    return null;
                }

                var result = response.JsonBody.RootElement;
                var schedule = AnalysisResult(result);

                return schedule;
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetServiceSchedule失败 - {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetServiceSchedule失败 - {ex.Message} - {ex.StackTrace}");
                return null;
            }
        }

        private List<Schedule> AnalysisResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);
            var code = dicResult["code"].ToInt();
            var message = dicResult["message"].NotNullString();
            if (code != 200)
            {
                throw new HttpException($"code = {code}, message = {message}");
            }

            var dataJsonElement = jsonElement.GetProperty("data");
            var dataList = JsonAnalysis.JsonToDicList(dataJsonElement);

            var scheduleList = new List<Schedule>();
            foreach (var data in dataList)
            {
                var schedules = CheckBuildSchedule(data);
                if (schedules.HasItem())
                {
                    scheduleList.AddRange(schedules);
                }
            }

            if (scheduleList.HasItem())
            {
                MainSession.AddMiaoSession("Schedule", scheduleList);
                MainSession.SetStatus(MiaoProgress.MiaoGet);
            }

            MainSession.PrintLogEvent.Publish(this, "查到苗基本信息");

            return scheduleList;
        }

        private List<Schedule> CheckBuildSchedule(Dictionary<string, object> data)
        {
            try
            {
                var scheduleList = new List<Schedule>();

                var date = data["date"].NotNullString();
                var serviceStart = data["startTime"].NotNullString();
                var serviceEnd = data["endTime"].NotNullString();

                var morning = data["morning"].NotNullString().ToDic();
                var morningSchedule = BuildSchedule(morning, date, serviceStart, serviceEnd);
                if (morningSchedule != null)
                {
                    morningSchedule.Half = "morning";
                    scheduleList.Add(morningSchedule);
                }

                var afternoon = data["afternoon"].NotNullString().ToDic();
                var afternoonSchedule = BuildSchedule(afternoon, date, serviceStart, serviceEnd);
                if (afternoonSchedule != null)
                {
                    afternoonSchedule.Half = "afternoon";
                    scheduleList.Add(afternoonSchedule);
                }

                var night = data["night"].NotNullString().ToDic();
                var nightSchedule = BuildSchedule(night, date, serviceStart, serviceEnd);
                if (nightSchedule != null)
                {
                    nightSchedule.Half = "night";
                    scheduleList.Add(nightSchedule);
                }

                return scheduleList;
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"CheckBuildSchedule失败 - {ex.Message} - {ex.StackTrace}");
                return null;
            }
        }

        private Schedule BuildSchedule(Dictionary<string, string> scheduleSource, string date, string serviceStart, string serviceEnd)
        {
            if (scheduleSource == null)
            {
                return null;
            }

            var startTime = scheduleSource["startTime"];
            if (startTime == null)
            {
                return null;
            }

            var endTime = scheduleSource["endTime"];
            if (endTime == null)
            {
                return null;
            }
            var open = scheduleSource["open"];
            if (open != "true")
            {
                return null;
            }

            var full = scheduleSource["full"];
            if (full != "false")
            {
                return null;
            }

            return new Schedule
            {
                Date = date,
                StartTime = startTime,
                EndTime = endTime,
                ServiceStartTime = serviceStart,
                ServiceEndTime = serviceEnd
            };
        }
    }
}
