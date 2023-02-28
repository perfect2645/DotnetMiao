using Base.viewmodel.status;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Jikong.appointment;
using Jikong.common;
using Jikong.login;
using Jikong.session;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;
using Newtonsoft.Json;

namespace Jikong.search
{
    internal class MiaoController : HttpClientBase
    {

        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync(string date, JikongLogin user)
        {
            Date = date;
            Task.Factory.StartNew(() => SearchMiao(date, user));
        }

        public bool SearchMiao(string date, JikongLogin user)
        {
            Date = date;
            try
            {
                var content = new MiaoContent(user, date);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败 - {response?.Message},请检查参数");
                    return false;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                if (code != "0" || msg != "操作完成")
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:code={code}, message: {msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                return SaveMiao(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }


        private bool SaveMiao(JsonElement data, JikongLogin user)
        {
            var summaryList = JsonAnalysis.JsonToDicList(data);
            if (!summaryList.HasItem())
            {
                Log($"没开始放苗");
                return false;
            }

            //var summary = summaryList.FirstOrDefault() as Dictionary<string, object>;

            //var list = summary["list"].NotNullString();
            //var scheduleList = JsonConvert.DeserializeObject<List<Schedule>>(list);

            var list = JsonConvert.SerializeObject(summaryList, Formatting.Indented);
            var scheduleList = JsonConvert.DeserializeObject<List<Schedule>>(list);

            BuildOrderList(scheduleList);

            return true;
        }

        private void BuildOrderList(List<Schedule> scheduleList)
        {
            var orderList = new List<Order>();

            foreach (var user in MainSession.Users)
            {
                var userName = user.UserName;
                foreach (var schedule in scheduleList)
                {
                    Order orderWithTime = BuildOneOrder(user, Date, schedule);
                    orderList.Add(orderWithTime);
                }
            }
            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);
        }

        private Order BuildOneOrder(JikongLogin user, string date, Schedule schedule)
        {
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var doctorName = MainSession.PlatformSession.GetString(Constants.DoctorName);
            var doctorId = MainSession.PlatformSession.GetString(Constants.DocId);
            return new Order
            {
                UserId = user.UserId,
                UserName = user.UserName,
                User = user,
                AmOrPm = schedule.amPm,
                ItemName = doctorName,
                ItemCode = doctorId,
                MinuteHourRegTotal = schedule.minuteHourRegTotal,
                ScheduleCode = schedule.scheduleCode,
                ScheduleInfoCode = schedule.scheduleInfoCode,
                VisitDate = Date,
                VisitTime = schedule.minuteHourInfo,
            };
        }
    }
}
