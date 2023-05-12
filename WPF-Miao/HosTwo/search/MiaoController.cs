using HosTwo.appointment;
using HosTwo.login;
using HosTwo.session;
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

namespace HosTwo.search
{
    internal class MiaoController : HttpClientBase
    {

        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync(string date)
        {
            Date = date;
            Task.Factory.StartNew(() => SearchMiao(date));
        }

        public bool SearchMiao(string date)
        {
            Date = date;
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser, date);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var responseResult = root.GetProperty("responseResult");
                if (responseResult.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return false;
                }
                var isSuccess = responseResult.GetProperty("isSuccess").GetString();
                if (isSuccess != "1")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: isSuccess = {isSuccess}");
                    return false;
                }

                var resourceList = root.GetProperty("docResourceResourceList");

                return CheckSaveResource(resourceList);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveResource(JsonElement resourceListData)
        {
            var resourceList = JsonAnalysis.JsonToDicList(resourceListData);
            if (!resourceList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            var availableResources = resourceList.Where(r => r.GetString("isAvailable") == "1").ToList();
            if (!availableResources.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return false;
            }

            BuildOrderList(availableResources);

            return true;
        }

        private void BuildOrderList(List<Dictionary<string, object>> resources)
        {
            var orderList = new List<Order>();

            foreach (var resource in resources)
            {
                Order orderWithTime = BuildOneOrder(resource);
                orderList.Add(orderWithTime);
            }
            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);
        }

        private Order BuildOneOrder(Dictionary<string, object> resource)
        {
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var hospitalName = MainSession.PlatformSession.GetString(Constants.HospitalName);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var docId = MainSession.PlatformSession.GetString(Constants.DocCode);
            var docName = MainSession.PlatformSession.GetString(Constants.DocName);
            var docDuty = MainSession.PlatformSession.GetString(Constants.DocDuty);

            var day = resource.GetString("day");
            var timeEnd = resource.GetString("timeEnd");


            return new Order
            {

            };
        }
    }
}
