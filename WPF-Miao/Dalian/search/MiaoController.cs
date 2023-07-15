using Base.viewmodel.status;
using Dalian.appointment;
using Dalian.session;
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
using Dalian.common;
using Newtonsoft.Json;

namespace Dalian.search
{
    internal class MiaoController : DalianController
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
                    MainSession.PrintLogEvent.Publish(this, $"SearchDates - {response?.Message},请检查参数");
                    return false;
                }

                var root = response.JsonBody.RootElement;

                var status = root.GetProperty("status").GetInt32();
                var msg = root.GetProperty("msg").GetString();
                if (status != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: status={status}, msg={msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: results is empty");
                    return false;
                }

                var dateMiaoList = data.GetProperty("regPoints").GetProperty(date);

                return CheckSaveSchedule(dateMiaoList);
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
                return false;
            }

            var avaliableSchedules = scheduleInfoList.Where(s => s.GetString("rmngNum").ToInt() > 0).ToList();
            if (!avaliableSchedules.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"{Date} - 没有可用苗");
                return false;
            }

            var orderList = new List<Order>();

            foreach (var schedule in scheduleInfoList)
            {
                var order = BuildOrder(schedule);
                orderList.Add(order);
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

        private Order BuildOrder(Dictionary<string, object> schedule)
        {
            var regLevelId = MainSession.PlatformSession.GetString(Constants.RegLevelId);
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            var feeInfo = JsonConvert.DeserializeObject<dynamic>(schedule.GetString("regFeeDetails"));
            var fee = feeInfo[0].fee;

            var order = new Order
            {
                PointId = schedule.GetString("pointId"),
                PointName = schedule.GetString("pointName"),
                PointDate = schedule.GetString("pointDate"),
                RegLevelId = regLevelId,
                RegLevelName = schedule.GetString("regLevelName"),
                DeptId = deptId,
                VisitTime = schedule.GetString("startTime"),
                BeginTime = schedule.GetString("startTime"),
                EndTime = schedule.GetString("endTime"),
                DiagnoseFee = fee,
                NoonId = schedule.GetString("noonId"),
                NoonName = schedule.GetString("noonName"),
            };

            return order;
        }
    }
}
