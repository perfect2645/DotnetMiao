using Base.viewmodel.status;
using jieyang.appointment;
using jieyang.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
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

namespace jieyang.search
{
    internal class DoctorInfoController : HttpClientBase
    {
        public DoctorInfoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync()
        {
            if (MainSession.GetStatus() == MiaoProgress.MiaoGet)
            {
                return;
            }
            Task.Factory.StartNew(() => SearchMiao());
        }

        private void SearchMiao()
        {
            var url = "https://ctmingyi.com:18082/api/hospital/doctorInfo";

            var content = new DoctorInfoContent(url);
            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (MainSession.GetStatus() == MiaoProgress.MiaoGet)
                {
                    return;
                }

                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"没查到苗{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{msg}");
                if ("0".Equals(code))
                {
                    var miaoInfo = root.GetProperty("obj");
                    SaveMiaoInfo(miaoInfo);
                    
                    return;
                }
                Log(msg);

            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SaveMiaoInfo(JsonElement miaoElement)
        {
            if (miaoElement.ValueKind == JsonValueKind.Null)
            {
                Log($"未查到苗miaoInfo is null");
                return;
            }

            var defaultMiao = miaoElement.EnumerateArray().FirstOrDefault();
            var scheduleElement = defaultMiao.GetProperty("scheduleList");
            if (scheduleElement.ValueKind == JsonValueKind.Null)
            {
                Log($"没开始放苗scheduleElement is null");
                return;
            }

            var scheduleList = JsonAnalysis.JsonToDicList(scheduleElement);
            if (scheduleList == null)
            {
                Log($"没开始放苗scheduleList is null");
                return;
            }

            var avaliableScheduleList = scheduleList.Where(x => x["reservation"].NotNullString().ToBool()).ToList();
            if (!avaliableScheduleList.HasItem())
            {
                //Log($"查到{scheduleList.Count}个schedule, reservation=false但是没有放苗");
                return;
            }

            MainSession.PrintLogEvent.Publish(this, $"查到苗- 放{avaliableScheduleList.Count}天");
            MainSession.SetStatus(MiaoProgress.MiaoGet);

            BuildOrderList(avaliableScheduleList);
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
