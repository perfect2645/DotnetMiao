using Base.viewmodel.status;
using Puzhou.appointment;
using Puzhou.session;
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

namespace Puzhou.search
{
    internal class MiaoController : HttpClientBase
    {
        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchMiao()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var success = root.GetProperty("success").GetBoolean();
                var msg = root.GetProperty("msg").GetString();
                if (!success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败: success={success}, msg = {msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败: results is empty");
                    return false;
                }

                var list = data.GetProperty("list");

                return CheckSaveSchedule(list);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule(JsonElement scheduleInfoData)
        {
            var scheduleInfo = JsonAnalysis.JsonToDic(scheduleInfoData);

            var orderList = new List<Order>();

            var amListStr = scheduleInfo.GetString("amList");
            if (!string.IsNullOrEmpty(amListStr))
            {
                var amOrderList = BuildOrderList(amListStr);
                orderList.AddRange(amOrderList);
            }

            var pmListStr = scheduleInfo.GetString("pmList");
            if (!string.IsNullOrEmpty(pmListStr))
            {
                var pmOrderList = BuildOrderList(pmListStr);
                orderList.AddRange(pmOrderList);
            }

            if (!orderList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return false;
            }

            MainSession.SetStatus(MiaoProgress.MiaoDetailGet);

            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);

            return true;
        }

        private List<Order> BuildOrderList(string scheduleListStr)
        {
            var orderList = new List<Order>();
            var scheduleList = JsonAnalysis.JsonToDicList(scheduleListStr);
            if (!scheduleList.HasItem())
            {
                return orderList;
            }

            var availableScheduleList = scheduleList.Where(x => x.GetString("peopleNumber").ToInt() > 0).ToList();
            if (!availableScheduleList.HasItem())
            {
                return orderList;
            }

            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var hosName = MainSession.PlatformSession.GetString(Constants.HospitalName);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            foreach (var schedule in availableScheduleList)
            {
                var timeNo = schedule.GetString("timeNo");
                var order = new Order
                {
                    HospitalCode = hosId,
                    MakeAnAppointment = Date,
                    TimeNo = timeNo,
                    VaccineInfoId = deptId,
                    Address = hosName
                };
                orderList.Add(order);
            }

            return orderList;
        }
    }
}
