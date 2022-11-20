using Base.viewmodel.status;
using HttpProcessor.Client;
using suiyang.appointment;
using suiyang.common;
using suiyang.session;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace suiyang.search
{
    internal class GetMiaoController : HttpClientBase
    {
        public IntervalOnTime IntervalOnTime { get; private set; }
        public string Date { get; set; }
        public GetMiaoController(HttpClient httpClient) : base(httpClient)
        {
            IntervalOnTime = new IntervalOnTime(GetMiao, Date, 200);
        }

        public void GetMiaoAsync()
        {
            //Task.Factory.StartNew(() => GetMiao());
            IntervalOnTime.StartIntervalOntime();
        }

        private void GetMiao()
        {
            try
            {
                var dept = MainSession.PlatformSession.GetString(Constants.DeptId);
                var dc = DateTimeUtil.GetTimeStamp();
                var url = $"http://www.jxy-tech.com/api/v1/locations/1/canbeappointed/businesses/{dept}/availableDates?_dc={dc}";
                var content = new SuiyangBaseContent(url);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetMiao - {response?.Message},请检查参数");
                }
                var success = response.JsonBody.RootElement.GetProperty("success").NotNullString().ToBool();
                if (success)
                {
                    var data = response.JsonBody.RootElement.GetProperty("data");
                    SaveMiaoInfo(data);
                    MainSession.SetStatus(MiaoProgress.ReadyForSearch);
                    return;
                }
                MainSession.SetStatus(MiaoProgress.Init);
                var msg = response.JsonBody.RootElement.GetProperty("message").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{msg}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetFamily异常{ex.Message}");
            }
        }

        private void SaveMiaoInfo(JsonElement data)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(data);
            foreach (var schedule in scheduleList)
            {
                var availableQty = schedule.GetString("availableQty").ToInt();
                if (availableQty > 0)
                {
                    IntervalOnTime.StopInterval();
                    PublishMiaoGet(Date);
                    MainSession.PrintLogEvent.Publish(null, $"查到苗，date={Date}");
                }
            }
        }

        private void PublishMiaoGet(string date)
        {
            var orderList = new List<Order>();

            var order = new Order
            {
                BtCode = MainSession.PlatformSession.GetString(Constants.DeptId),
                AppointDate = date,
                Barcode = MainSession.Auth,

            };

            var appointEventArgs = new AppointEventArgs
            {
                OrderList = orderList
            };
            MainSession.PrintLogEvent.Publish(this, $"查到{orderList.Count}支苗，开始预备预约");
            Task.Factory.StartNew(() =>
            {
                MainSession.AppointEvent.Publish(null, appointEventArgs);
            });
        }
    }
}
