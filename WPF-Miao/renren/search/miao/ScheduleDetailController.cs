using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using renren.appointment;
using renren.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;

namespace renren.search.miao
{
    internal class ScheduleDetailController : HttpClientBase
    {

        public List<Schedule> ScheduleList { get; private set; }

        public ScheduleDetailController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BulkGetDetailAsync(List<Schedule> scheduleList)
        {
            ScheduleList = scheduleList;

            var defa = ScheduleList.FirstOrDefault();
            Task.Factory.StartNew(() => GetScheduleDetail(defa));
            foreach (var schedule in ScheduleList)
            {
                Task.Factory.StartNew(() => GetScheduleDetail(schedule));
                schedule.IntervalOnTime.StartIntervalOntime(() =>
                {
                    Task.Factory.StartNew(() => GetScheduleDetail(schedule));
                });
            }
        }

        private bool GetScheduleDetail(Schedule schedule)
        {
            var url = "https://www.medic.ren/PM-server/mobserviceTimeDef/getServiceScheduleDetail";

            var content = new ScheduleDetailContent(url, schedule);

            content.BuildDefaultHeaders(Client);

            try
            {
                Log("开始查询ScheduleDetail");
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                Log("查询ScheduleDetail完成");
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetScheduleDetail - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement;
                AnalysisResult(result, schedule);

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

        private List<Order> AnalysisResult(JsonElement jsonElement, Schedule schedule)
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

            var orderList = new List<Order>();
            foreach (var data in dataList)
            {
                var orders = BuildOrderList(data, schedule);
                orderList.AddRange(orders);
            }

            if (!orderList.HasItem())
            {
                return null;
            }

            schedule.IntervalOnTime.StopInterval();
            orderList = orderList.DisorderItems();

            MainSession.PrintLogEvent.Publish(this, dataList,"查到苗详细信息");
            var appointEventArgs = new AppointEventArgs
            {
                OrderList = orderList
            };
            MainSession.AppointEvent.Publish(null, appointEventArgs);

            return orderList;
        }

        private List<Order> BuildOrderList(Dictionary<string, object> data, Schedule schedule)
        {
            try
            {
                var orderList = new List<Order>();

                var open = data["open"].NotNullString().ToBool();
                var full = data["full"].NotNullString().ToBool();
                if (open && !full)
                {
                    orderList.Add(new Order
                    {
                        BookingDate = schedule.Date,
                        DateId = data["id"].NotNullString(),
                        StartTime = data["startTime"].NotNullString(),
                        EndTime = data["endTime"].NotNullString(),
                        ServiceStartTime = schedule.ServiceStartTime,
                        ServiceEndTime = schedule.ServiceEndTime
                    });
                }

                return orderList;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"BuildOrderList失败 - {ex.Message} - {ex.StackTrace}");
                return null;
            }
        }

    }
}
