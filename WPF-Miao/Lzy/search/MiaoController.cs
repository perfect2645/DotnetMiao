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
using HttpProcessor.HtmlAnalysis;

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

                return CheckSaveSchedule(root, tempOrder);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule(string htmlStr, Order tempOrder)
        {
            var htmlDoc = new HtmlDoc(htmlStr);

            var timeBoxXpath = "//div[@class=\"time-box\"]";
            var timeBoxDoc = htmlDoc.GetInnerDoc(timeBoxXpath);
            if (timeBoxDoc == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"SearchMiao - cannot get [timeBoxDoc]");
                return false;
            }

            var timeItemXpath = "//div[@class=\"time-item\"]";
            var timeItems = timeBoxDoc.SearchNodes(timeItemXpath);
            if (!timeItems.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"SearchMiao - cannot get [timeItems]");
                return false;
            }

            bool hasMiao = false;
            foreach(var timeItem in timeItems)
            {
                var miaoSchedule = new MiaoSchedule(timeItem.InnerHtml);
                if (miaoSchedule.IsAvailable)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查到苗 - timeid = {miaoSchedule.TimeId}");
                    BuildOrderList(miaoSchedule, tempOrder);
                    hasMiao = true;
                }
            }

            return hasMiao;
        }

        private void BuildOrderList(MiaoSchedule miaoSchedule, Order tempOrder)
        {
            var orderList = new List<Order>();

            var order = new Order()
            {
                TimeId = miaoSchedule.TimeId,
                Date = tempOrder.Date,
                DeptId = tempOrder.DeptId,
            };

            orderList.Add(order);

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);
        }
    }
}
