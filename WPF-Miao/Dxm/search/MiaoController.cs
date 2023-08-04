using Base.viewmodel.status;
using Dxm.appointment;
using Dxm.session;
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

namespace Dxm.search
{
    internal class MiaoController : HttpClientBase
    {
        public string Date { get; set; }
        public Order OrderTemplate { get; set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchMiao(string date, Order orderTemplate)
        {
            OrderTemplate = orderTemplate;
            Date = date;
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser, date);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: code={code}");
                    return false;
                }

                var result = root.GetProperty("result");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: results is empty");
                    return false;
                }

                return CheckSaveSchedule(result);
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

            var medName = scheduleInfo.GetString("medName");
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, medName);

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
            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);

            foreach (var schedule in availableScheduleList)
            {
                var timeNo = schedule.GetString("timeNo");
                var order = new Order
                {
                    HospitalCode = hosId,
                    MakeAnAppointment = Date,
                    TimeNo = timeNo,
                    Address = hosName,
                    DeptId = OrderTemplate.DeptId,
                    DeptName = deptName,
                    FacName = OrderTemplate.FacName,
                    Price = OrderTemplate.Price,
                    PriceTxt = OrderTemplate.PriceTxt,
                    Spec = OrderTemplate.Spec,
                    TimeType = schedule.GetString("timeType"),
                    VaccineType = OrderTemplate.VaccineType
                };
                orderList.Add(order);
            }

            return orderList;
        }
    }
}
