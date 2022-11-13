using Base.viewmodel.status;
using chutian.appointment;
using chutian.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
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

namespace chutian.search
{
    internal class DoctorInfoController : HttpClientBase
    {
        public DoctorInfoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync()
        {
            if (MainSession.GetStatus() == MiaoProgress.MiaoGet)
            {
                return;
            }
            Task.Factory.StartNew(() => SearchMiao());
        }

        private void SearchMiao()
        {
            var url = "https://ctmingyi.com:18082/api/hospital/doctorInfo";

            var content = new DoctorInfoContent(url);
            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (MainSession.GetStatus() == MiaoProgress.MiaoGet)
                {
                    return;
                }

                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"没查到苗{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var success = root.GetProperty("success").NotNullString().ToBool();
                var msg = root.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{msg}");
                if ("0".Equals(code) && success)
                {
                    var miaoInfo = root.GetProperty("obj");
                    SaveMiaoInfo(miaoInfo);
                    
                    return;
                }

            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SaveMiaoInfo(JsonElement miaoElement)
        {
            if (miaoElement.ValueKind == JsonValueKind.Null)
            {
                Log($"未查到苗miaoInfo is null");
                return;
            }

            var defaultMiao = miaoElement.EnumerateArray().FirstOrDefault();
            var scheduleElement = defaultMiao.GetProperty("scheduleList");
            if (scheduleElement.ValueKind == JsonValueKind.Null)
            {
                Log($"没开始放苗scheduleElement is null");
                return;
            }

            var scheduleList = JsonAnalysis.JsonToDicList(scheduleElement);
            if (scheduleList == null)
            {
                Log($"没开始放苗scheduleList is null");
                return;
            }

            MainSession.PrintLogEvent.Publish(this, $"查到苗- 放{scheduleList.Count}天");
            MainSession.SetStatus(MiaoProgress.MiaoGet);
            BuildOrderList(scheduleList);
        }

        private void BuildOrderList(List<Dictionary<string, object>> scheduleList)
        {
            var orderList = new List<Order>();
            foreach (var schedule in scheduleList)
            {
                orderList.Add(new Order
                {

                });
            }

            orderList = orderList.DisorderItems();

            var appointEventArgs = new AppointEventArgs
            {
                OrderList = orderList
            };
            MainSession.AppointEvent.Publish(null, appointEventArgs);
        }
    }
}
