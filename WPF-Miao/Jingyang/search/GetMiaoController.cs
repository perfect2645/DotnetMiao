using Base.viewmodel.status;
using HttpProcessor.Client;
using Jingyang.appointment;
using Jingyang.common;
using Jingyang.session;
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

namespace Jingyang.search
{
    internal class GetMiaoController : HttpClientBase
    {
        public IntervalOnTime IntervalOnTime { get; private set; }
        public string Date { get; private set; }
        public JingyangBaseContent Content { get; private set; }
        public GetMiaoController(HttpClient httpClient) : base(httpClient)
        {
            IntervalOnTime = new IntervalOnTime(GetMiao, Date, 200);
        }

        internal void BuildContent(string date)
        {
            Date = date;
            var dc = DateTimeUtil.GetTimeStamp();
            var dept = MainSession.PlatformSession.GetString(Constants.DeptId);
            var url = $"http://www.jxy-tech.com/api/v1/locations/1/canbeappointed/businesses/{dept}/availableTimeRanges?_dc={dc}&date={Date}";
            var content = new JingyangBaseContent(url);
            
            Content = content;
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
                var response = GetStringAsync(Content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetMiao - {response?.Message},请检查参数");
                }
                var success = response.JsonBody.RootElement.GetProperty("success").NotNullString().ToBool();
                if (success)
                {
                    var data = response.JsonBody.RootElement.GetProperty("data");
                    SaveMiaoInfo(data);
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
            if (!scheduleList.HasItem())
            {
                Log("scheduleList is empty, 没查到苗");
            }

            var orderList = new List<Order>();

            foreach (var schedule in scheduleList)
            {
                var availableQty = schedule.GetString("availableQty").ToInt();
                var beginTime = schedule.GetString("beginTime");
                var endTime = schedule.GetString("endTime");

                if (availableQty > 0)
                {
                    IntervalOnTime.StopInterval();
                    var orderPerSchedule = BuildOrders(beginTime, endTime);
                    orderList.AddRange(orderPerSchedule);
                    MainSession.PrintLogEvent.Publish(null, $"查到苗，date={Date}");
                }

                if (!orderList.HasItem())
                {
                    MainSession.PrintLogEvent.Publish(null, $"没苗了，现在在捡漏， date={Date}");
                    return;
                }

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

        private List<Order> BuildOrders(string beginTime, string endTime)
        {
            var orderList = new List<Order>();
            var userList = MainSession.PlatformSession["userList"] as List<Dictionary<string, object>>;
            foreach(var userInfo in userList)
            {
                var order = new Order
                {
                    BtCode = MainSession.PlatformSession.GetString(Constants.DeptId),
                    AppointDate = Date,
                    BeginTime = beginTime,
                    EndTime = endTime,
                    Barcode = MainSession.Auth.Replace("Bearer ", string.Empty),
                    AddressId = userInfo.GetString("id").ToInt(),
                    Identity = userInfo.GetString("identity"),
                    Phone = userInfo.GetString("mobile"),
                    IdName = userInfo.GetString("firstName"),
                };

                orderList.Add(order);
            }


            return orderList;
        }
    }
}
