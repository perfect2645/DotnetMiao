using Base.viewmodel.status;
using Gzjk.appointment;
using Gzjk.session;
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

namespace Gzjk.search
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
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var status = root.GetProperty("status").GetInt32();
                if (status != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: status={status}");
                    return false;
                }

                var list = root.GetProperty("list");
                if (list.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: list is empty");
                    return false;
                }

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
                orderList.Add(order);
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
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var mxiId = schedule.GetString("mxid");

            var order = new Order
            {
                Date = date,
                Mxid = mxiId,
                Pid = deptId,
            };

            return order;
        }
    }
}
