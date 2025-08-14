using Jingjiang.appointment;
using Jingjiang.login;
using Jingjiang.session;
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
using Newtonsoft.Json;
using Utils.stringBuilder;

namespace Jingjiang.search
{
    internal class MiaoController : HttpClientBase
    {

        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync()
        {
            Task.Factory.StartNew(() => SearchMiao());
        }

        public bool SearchMiao()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var msg = root.GetProperty("msg").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: code={code}, msg={msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: data is empty");
                    return false;
                }

                return CheckSaveResource(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveResource(JsonElement dataElement)
        {
            var vaccineDay = JsonAnalysis.JsonToDic(dataElement);
            if (!vaccineDay.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            var date = vaccineDay.FirstOrDefault().Key;

            var vaccineList = vaccineDay.FirstOrDefault().Value.NotNullString().ToObjDicList();


            if (!vaccineList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            return BuildOrderList(vaccineList);
        }

        private bool BuildOrderList(List<Dictionary<string, object>> vaccineDayList)
        {
            var orderList = new List<Order>();

            var availableVaccine = vaccineDayList.Where(x => x.GetString("wyyNum").ToInt() > 0).ToList();
            if (!availableVaccine.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return false;
            }

            foreach (var vaccineDay in availableVaccine)
            {
                var datetimeId = vaccineDay.GetString("datetimeId");
                var ywId = vaccineDay.GetString("ywId");
                var yyDate = vaccineDay.GetString("yyDate");
                var dwCode = MainSession.PlatformSession.GetString(Constants.DwCode);

                var order = new Order
                {
                    DatetimeId = datetimeId,
                    DwCode = dwCode,
                    YwDateId = yyDate,
                    YwId = ywId,
                };

                orderList.Add(order);
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
    }
}
