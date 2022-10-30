using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HttpProcessor.Response;
using hys020.appointment;
using hys020.session;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;

namespace hys020.search
{
    internal class MiaoDetailController : HttpClientBase
    {
        private string departmentId = MainSession.PlatformSession[Constants.DeptId].NotNullString();

        public MiaoDetailController(HttpClient httpClient) : base(httpClient)
        {
        }


        internal Task SearchMiaoDetailAsync(string attId)
        {
            return Task.Factory.StartNew(() => SearchMiaoDetail(attId));
        }

        public void SearchMiaoDetail(string attId)
        {
            var url = $"http://www.hys020.com/home/doctorYyghMobileSectionDate_{attId}";

            var content = new MiaoDetailContent(url);
            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao Failed - {response.Message}");
                    return;
                }

                var result = response.JsonBody.RootElement;
                var numList = response.JsonBody.RootElement.GetProperty("numList");
                if (numList.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"未查到苗 - numList is null");
                    return;
                }
                AnalysisResult(numList);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }


        private void AnalysisResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDicList(jsonElement);

            var validMiaoList = dicResult.Where(x => x["regLeft"].ToInt() > 0).ToList();

            if (!validMiaoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没查到苗");
                return;
            }

            var orderList = new List<Order>();

            foreach (var miao in validMiaoList)
            {
                var order = BuildOrder(miao);
                if (order != null)
                {
                    orderList.Add(order);
                }
            }

            orderList = orderList.DisorderItems();

            MainSession.SetStatus(MiaoProgress.AppointStart);
            var appointEventArgs = new AppointEventArgs
            {
                OrderList = orderList
            };
            MainSession.AppointEvent.Publish(null, appointEventArgs);

        }

        private Order BuildOrder(Dictionary<string, object> miao)
        {
            var time = miao["regPhase"].NotNullString();
            var order = new Order()
            {
                DepartmentId = departmentId,
                AttId = miao["regAttId"].NotNullString(),
                TimeRangeEncode = UnicodeConverter.Encode(time),
            };

            return order;
        }
    }
}
