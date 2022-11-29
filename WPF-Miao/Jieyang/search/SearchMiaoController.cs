using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using jieyang.appointment;
using jieyang.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;

namespace jieyang.search
{
    internal class SearchMiaoController : HttpClientBase
    {
        public bool IsMiaoGet { get; set; }
        public string Date { get; private set; }
        public SearchMiaoContent SearchMiaoContent { get; private set; }

        public SearchMiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildContent(string timeRange)
        {
            var employeeId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            Date = DateTimeUtil.GetTomorrow();
            var timeType = UnicodeConverter.Encode(timeRange, true);

            var urlTemplate = $"http://wx1936.cnhis.cc/wx/dept/scheduleDoctorRange.htm?employeeId={employeeId}&deptId={deptId}&date={Date}&timeType={timeType}&newVersion=true";

            SearchMiaoContent = new SearchMiaoContent(urlTemplate);
            SearchMiaoContent.BuildDefaultHeaders(Client);
        }

        public async Task<bool> SearchMiaoAsync()
        {
            if (IsMiaoGet)
            {
                return true;
            }
            return await Task.Factory.StartNew(() => SearchMiao());
        }

        private bool SearchMiao()
        {

            try
            {
                HttpDicResponse response = GetStringAsync(SearchMiaoContent).Result;
                if (IsMiaoGet)
                {
                    return true;
                }

                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"没查到苗{response?.Message}");
                    return false;
                }

                var root = response.JsonBody.RootElement;
                var result = root.GetProperty("result").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{result}");
                if ("Success".Equals(result))
                {
                    var miaoList = root.GetProperty("list");
                    return SaveMiaoInfo(miaoList);
                }
                return false;
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

            var avaliableScheduleList = scheduleList.Where(x => x["count"].NotNullString().ToInt() > 0).ToList();
            if (!avaliableScheduleList.HasItem())
            {
                Log($"查到{scheduleList.Count}个苗,但是没有可用苗");
                return false;
            }

            MainSession.PrintLogEvent.Publish(this, $"查到苗- 放{avaliableScheduleList.Count}天");
            IsMiaoGet = true;

            BuildOrderList(avaliableScheduleList);


            return true;
        }

        private void BuildOrderList(List<Dictionary<string, object>> scheduleList)
        {
            var baseOrderList = new List<Order>();

            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var doctorName = MainSession.PlatformSession.GetString(Constants.DoctorName);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var hosName = MainSession.PlatformSession.GetString(Constants.HospitalName);
            var userName = MainSession.PlatformSession.GetString(Constants.UserName);
            var userId = MainSession.PlatformSession.GetString(Constants.UserId);
            var amount = MainSession.PlatformSession.GetString(Constants.AppointAmount);

            foreach (var schedule in scheduleList)
            {
                baseOrderList.Add(new Order
                {
                    Regtype = schedule.GetString("registerTypeOriginalId"),
                    DeptId = deptId,
                    DeptName = deptName,
                    DoctorId = doctorId,
                    DoctorName = doctorName,
                    OrgName = hosName,
                    AppointAmount = amount,
                    AppointDate = Date,
                    TimeRange = schedule.GetString("timeRangeShow"),
                    ScheduleId = schedule.GetString(Constants.ScheduleId),
                    Bco01 = schedule.GetString("bco01"),
                });
            }

            baseOrderList = baseOrderList.DisorderItems();

            var appointEventArgs = new OrderEventArgs
            {
                OrderList = baseOrderList
            };
            MainSession.OrderEvent.Publish(null, appointEventArgs);


            //foreach (var user in MainSession.UserSession.Users)
            //{
            //    var userInfo = user.Value as Dictionary<string, object>;
            //    BuildOrdersForOneUser(userInfo, baseOrderList);
            //}
        }

        private void BuildOrdersForOneUser(Dictionary<string, object> userInfo, List<Order> baseOrderList)
        {
            var userId = userInfo.GetString(Constants.UserId);
            var userName = userInfo.GetString("familyName");
            var phone = userInfo.GetString("familyPhone");

            var orderList = new List<Order>();

            foreach (var baseOrder in baseOrderList)
            {
                var order = new Order();
                order.DoctorId = baseOrder.DoctorId;
                order.ScheduleId = baseOrder.ScheduleId;

                Log($"build order - {order.ToLogString()}");
                orderList.Add(order);
            }

            orderList = orderList.DisorderItems();

            var appointEventArgs = new OrderEventArgs
            {
                OrderList = orderList
            };
            MainSession.OrderEvent.Publish(null, appointEventArgs);
        }
    }
}
