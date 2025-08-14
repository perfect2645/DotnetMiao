using HttpProcessor.Client;
using Newtonsoft.Json;
using Rika.appointment;
using Rika.session;
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

namespace Rika.search
{
    internal class MiaoController : HttpClientBase
    {

        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<bool> SearchMiaoAsync(int index)
        {
            return Task.Factory.StartNew(() => SearchMiao(index));
        }

        public bool SearchMiao(int index)
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new DateContent(defaultUser, index);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                if (root.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchByDate失败: root is empty");
                    return false;
                }
                var resultDate = root.GetProperty("rq").GetString();

                var data = root.GetProperty("data");

                return CheckSaveSchedule(resultDate, data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule(string resultDate, JsonElement dataElement)
        {
            var vaccineDayList = JsonAnalysis.JsonToDicList(dataElement);
            if (!vaccineDayList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            var defaultVaccine = vaccineDayList.FirstOrDefault();

            var orderList = new List<Order>();
            var order = new Order
            {

            };

            orderList.Add(order);

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);
            return true;
        }

        private bool BuildOrderList(List<Dictionary<string, object>> vaccineDayList)
        {
            var orderList = new List<Order>();

            foreach (var vaccineDay in vaccineDayList)
            {
                var dayId = vaccineDay.GetString("id");
                var timeList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(vaccineDay.GetString("vaccineDayNum"));

                var dayVaccineBookedNum = vaccineDay.GetString("dayVaccineBookedNum").ToInt();
                //check if any vaccine is booked as the sign of start dayVaccineBookedNum
                //var isStarted = dayVaccineBookedNum > 0;
                //if (!isStarted)
                //{
                //    MainSession.PrintLogEvent.Publish(this, $"没开始,dayVaccineBookedNum={dayVaccineBookedNum}");
                //    continue;
                //}

                var dayVaccineRestNum = vaccineDay.GetString("dayVaccineRestNum").ToInt();
                MainSession.PrintLogEvent.Publish(this, $"开始了,已约={dayVaccineBookedNum}，剩余:{dayVaccineRestNum}");

                foreach (var timeItem in timeList)
                {
                    var forbidden = timeItem.GetString("forbidden");
                    if (forbidden.NotNullString() == "1")
                    {
                        continue;
                    }
                    var timeId = timeItem.GetString("id");
                    var orderWithTime = BuildOneOrder(dayId, timeId);
                    orderList.Add(orderWithTime);
                }
            }

            if(!orderList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return false;
            }

            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);

            return true;
        }

        private Order BuildOneOrder(string dayId, string timeId)
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            return new Order
            {
                VaccineDayId = dayId,
                VaccineDayNumId = timeId,
                VaccineId = deptId
            };
        }
    }
}
