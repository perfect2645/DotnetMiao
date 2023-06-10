using Base.viewmodel.status;
using B114.appointment;
using B114.session;
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
using B114.common;

namespace B114.search
{
    internal class MiaoController : B114Controller
    {
        public string Date { get; set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchMiao(string date)
        {
            try
            {
                Date = date;
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser, date);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }

                SaveSessionTime(response, defaultUser);

                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("resCode").GetInt32();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: code={code}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: results is empty");
                    return false;
                }

                return CheckSaveSchedule(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule(JsonElement scheduleInfoData)
        {
            var scheduleInfoList = JsonAnalysis.JsonToDicList(scheduleInfoData);
            if (!scheduleInfoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗");
            }

            var orderList = new List<Order>();

            foreach(var schedule in scheduleInfoList)
            {
                var detailListStr = schedule.GetString("detail");
                if (!string.IsNullOrEmpty(detailListStr))
                {
                    var scheduleOrderList = BuildOrderList(detailListStr);
                    orderList.AddRange(scheduleOrderList);
                }
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

                };
                orderList.Add(order);
            }

            return orderList;
        }
    }
}
