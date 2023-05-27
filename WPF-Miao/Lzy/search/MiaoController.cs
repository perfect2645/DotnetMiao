using Lzy.appointment;
using Lzy.session;
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

namespace Lzy.search
{
    internal class MiaoController : HttpClientBase
    {
        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<bool> SearchMiaoAsync(string date)
        {
            return await Task.Factory.StartNew(() => SearchMiao(date));
        }

        public bool SearchMiao(string date)
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
                var tempOrder = new Order
                {
                    Date = date,
                    DeptId = deptId,
                };
                var content = new MiaoContent(defaultUser, tempOrder);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.ContentStr == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.ContentStr;

                return CheckSaveSchedule();
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule()
        {

            //BuildOrderList(availableSchedules);

            return true;
        }

        private void BuildOrderList(List<Dictionary<string, object>> schedules)
        {
            var orderList = new List<Order>();

            foreach (var schedule in schedules)
            {
                Order orderWithTime = BuildOneOrder(schedule);
                orderList.Add(orderWithTime);
            }
            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);
        }

        private Order BuildOneOrder(Dictionary<string, object> schedule)
        {
            var beginTime = schedule.GetString("visitBeginTime");
            var endTime = schedule.GetString("visitEndTime");

            return new Order
            {

            };
        }
    }
}
