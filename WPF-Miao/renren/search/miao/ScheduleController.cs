using Base.viewmodel.status;
using HttpProcessor.Client;
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
        public ScheduleController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetServiceScheduleAsync()
        {
            try
            {
                var hasSchedule = await Task.Factory.StartNew(() => GetServiceSchedule());
                if (!hasSchedule.HasItem())
                {
                    return;
                }
                await Task.Factory.StartNew(() => GetScheduleDetail());
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

        private bool GetScheduleDetail()
        {
            var url = "https://www.medic.ren/PM-server/mobserviceTimeDef/getServiceScheduleDetail";

            var content = new ScheduleDetailContent(url);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetScheduleDetail - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement;
                AnalysisTeamDetail(result);

                return true;
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetScheduleDetail失败 - {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetScheduleDetail失败 - {ex.Message} - {ex.StackTrace}");
                return false;
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

            return scheduleList;

            if (scheduleList.HasItem())
            {
                MainSession.AddMiaoSession("schedule", scheduleList);
                MainSession.SetStatus(MiaoProgress.MiaoGet);
            }
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
                    scheduleList.Add(morningSchedule);
                }

                var afternoon = data["afternoon"].NotNullString().ToDic();
                var afternoonSchedule = BuildSchedule(morning, date, serviceStart, serviceEnd);
                if (afternoonSchedule != null)
                {
                    scheduleList.Add(afternoonSchedule);
                }

                var night = data["night"].NotNullString().ToDic();
                var nightSchedule = BuildSchedule(morning, date, serviceStart, serviceEnd);
                if (nightSchedule != null)
                {
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
                StartTime = startTime,
                EndTime = endTime,
            };
        }

        private void AnalysisTeamDetail(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);
            var code = dicResult["code"].ToInt();
            var message = dicResult["message"].NotNullString();

            var dataJsonElement = jsonElement.GetProperty("data");
            var data = JsonAnalysis.JsonToDic(dataJsonElement);

            if (code != 200 || !data.HasItem())
            {
                throw new HttpException($"code = {code}, message = {message}, data.count = {data?.Count}");
            }
            MainSession.HospitalSession.AddOrUpdate(data);
            //MainSession.PrintLogEvent.Publish(this, data, $"保存GetScheduleDetail");

            MainSession.PrintLogEvent.Publish(this, "查到苗");
        }
    }
}
