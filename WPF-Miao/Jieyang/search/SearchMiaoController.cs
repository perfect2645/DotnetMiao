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
        public SearchMiaoContent SearchMiaoContent { get; private set; }

        public SearchMiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildContent(string timeRange)
        {
            var employeeId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var date = DateTimeUtil.GetTomorrow();
            var timeType = UnicodeConverter.Encode(timeRange, true);

            var urlTemplate = $"http://wx1936.cnhis.cc/wx/dept/scheduleDoctorRange.htm?employeeId={employeeId}&deptId={deptId}&date={date}&timeType={timeType}&newVersion=true";

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
                    var miaoInfo = root.GetProperty("obj");
                    return SaveMiaoInfo(miaoInfo);
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

            var defaultMiao = miaoElement.EnumerateArray().FirstOrDefault();
            var scheduleElement = defaultMiao.GetProperty("scheduleList");
            if (scheduleElement.ValueKind == JsonValueKind.Null)
            {
                Log($"没开始放苗scheduleElement is null");
                return false; ;
            }

            var scheduleList = JsonAnalysis.JsonToDicList(scheduleElement);
            if (scheduleList == null)
            {
                Log($"没开始放苗scheduleList is null");
                return false; ;
            }

            var avaliableScheduleList = scheduleList.Where(x => x["reservation"].NotNullString().ToBool()).ToList();
            if (!avaliableScheduleList.HasItem())
            {
                //Log($"查到{scheduleList.Count}个schedule, reservation=false但是没有放苗");
                return false;
            }

            MainSession.PrintLogEvent.Publish(this, $"查到苗- 放{avaliableScheduleList.Count}天");
            MainSession.SetStatus(MiaoProgress.MiaoGet);

            BuildOrderList(avaliableScheduleList);


            IsMiaoGet = true;
            return true;
        }

        private void BuildOrderList(List<Dictionary<string, object>> scheduleList)
        {
            var baseOrderList = new List<Order>();

            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            foreach (var schedule in scheduleList)
            {
                baseOrderList.Add(new Order
                {
                    DoctorId = doctorId,
                    ScheduleId = schedule.GetString(Constants.Scheduleid),
                    Hospitalid = hospitalId,
                });
            }

            foreach (var user in MainSession.UserSession.Users)
            {
                var userInfo = user.Value as Dictionary<string, object>;
                BuildOrdersForOneUser(userInfo, baseOrderList);
            }
        }

        private void BuildOrdersForOneUser(Dictionary<string, object> userInfo, List<Order> baseOrderList)
        {
            var userId = userInfo.GetString(Constants.UserID);
            var familyId = userInfo.GetString(Constants.FamilyID);
            var userName = userInfo.GetString("familyName");
            var phone = userInfo.GetString("familyPhone");

            var orderList = new List<Order>();

            foreach (var baseOrder in baseOrderList)
            {
                var order = new Order();
                order.DoctorId = baseOrder.DoctorId;
                order.ScheduleId = baseOrder.ScheduleId;
                order.Hospitalid = baseOrder.Hospitalid;
                order.UserId = userId;
                order.FamilyId = familyId;
                order.UserName = userName;
                order.UserPhone = phone;

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
