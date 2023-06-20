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
using Utils.datetime;

namespace Puzhou.search
{
    internal class MiaoController : HttpClientBase
    {
        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchMiao(string date)
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser, date);
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

                return CheckSaveSchedule(list, date);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule(JsonElement scheduleListData, string date)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(scheduleListData);
            if (!scheduleList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有库存了");
                return false;
            }

            var orderList = new List<Order>();

            foreach(var schedule in scheduleList)
            {
                var order = BuildOrder(schedule, date);
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

        private Order BuildOrder(Dictionary<string, object> schedule, string date)
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var hosName = MainSession.PlatformSession.GetString(Constants.HospitalName);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var time = DateTimeUtil.GetDateTime(date, "g");


            var order = new Order
            {
                HosId = hosId,
                NumId = schedule.GetString("num_id"),
                ProjectId = hosId,
                SchId = schedule.GetString("sch_id"),
                Time = time,
            };

            return order;
        }
    }
}
