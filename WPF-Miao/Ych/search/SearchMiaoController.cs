using Base.viewmodel.status;
using HttpProcessor.Client;
using Ych.appointment;
using Ych.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace Ych.search
{
    public class SearchMiaoController : HttpClientBase
    {
        public bool IsMiaoGet { get; set; }
        public string Date { get; private set; }
        internal SearchMiaoContent SearchMiaoContent { get; private set; }
        public IntervalOnTime SearchInterval;

        public SearchMiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildContent(string date)
        {
            Date = date;
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var urlTemplate = $"http://101.34.141.250:9653/api/front/classification/tree?day={date}&type=1&yeWuId={deptId}";

            SearchMiaoContent = new SearchMiaoContent(urlTemplate);
            SearchMiaoContent.BuildDefaultHeaders(Client);

            SearchInterval = new IntervalOnTime(SearchMiaoAsync, $"{Date}", 300);
        }

        public async void SearchMiaoAsync()
        {
            if (IsMiaoGet)
            {
                SearchInterval.StopInterval();
            }
            await Task.Factory.StartNew(() => SearchMiao());
        }

        private bool SearchMiao()
        {

            try
            {
                HttpDicResponse response = GetStringAsync(SearchMiaoContent).Result;
                if (IsMiaoGet)
                {
                    SearchInterval.StopInterval();
                    return true;
                }

                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"获取miao失败{response?.Message}");
                    return false;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var message = root.GetProperty("message").NotNullString();
                if (!"200".Equals(code) || !message.Contains("成功"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取miao失败:code={code}, message={message}");
                    return false;
                }
                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取miao失败:code={code}, message={message}");
                    return false;
                }
                return SaveMiaoInfo(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool SaveMiaoInfo(JsonElement miaoElement)
        {
            if (miaoElement.ValueKind == JsonValueKind.Null)
            {
                Log($"未查到苗miaoInfo is null");
                return false;
            }

            var scheduleList = JsonAnalysis.JsonToDicList(miaoElement);
            if (scheduleList == null)
            {
                Log($"没开始放苗scheduleList is null");
                return false; ;
            }

            var avaliableScheduleList = scheduleList.Where(x => x["appointmentNum"].NotNullString().ToInt() > 0).ToList();
            if (!avaliableScheduleList.HasItem())
            {
                Log($"查到{scheduleList.Count}个苗,但是没有可用苗");
                return false;
            }

            IsMiaoGet = true;

            BuildOrderList(avaliableScheduleList);

            MainSession.SetStatus(MiaoProgress.MiaoGet);
            return true;
        }

        private void BuildOrderList(List<Dictionary<string, object>> scheduleList)
        {
            var baseOrderList = new List<Order>();

            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var userName = MainSession.PlatformSession.GetString(Constants.UserName);
            var userId = MainSession.PlatformSession.GetString(Constants.UserId);

            foreach (var schedule in scheduleList)
            {
                var startTime = schedule.GetString("startTime");
                var endTime = schedule.GetString("endTime");
                var timeRange = $"{startTime}-{endTime}";
                baseOrderList.Add(new Order
                {
                    UserId = userId,
                    UserName = userName,
                    AppointmentType = deptName,
                    ReservationDate = Date,
                    TimeId = schedule.GetString("id"),
                    YeWuId = deptId,
                    ReservationTime = timeRange,
                });
            }

            baseOrderList = baseOrderList.DisorderItems();

            var appointEventArgs = new OrderEventArgs
            {
                OrderList = baseOrderList
            };
            MainSession.OrderEvent.Publish(null, appointEventArgs);
        }
    }
}
