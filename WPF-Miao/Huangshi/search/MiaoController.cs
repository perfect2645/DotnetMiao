using HttpProcessor.Client;
using Huangshi.appointment;
using Huangshi.login;
using Huangshi.session;
using Newtonsoft.Json;
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

namespace Huangshi.search
{
    internal class MiaoController : HttpClientBase
    {

        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync(HuangshiLogin user, string date)
        {
            Date = date;
            Task.Factory.StartNew(() => SearchMiao(user, date));
        }

        public bool SearchMiao(HuangshiLogin user, string date)
        {
            Date = date;
            try
            {
                var content = new DateContent(user, date);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null && string.IsNullOrEmpty(response?.ContentStr))
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                return SaveMiao(root, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }


        private bool SaveMiao(JsonElement data, HuangshiLogin user)
        {
            var scheduleDic = JsonAnalysis.JsonToDicList(data);
            if (!scheduleDic.HasItem())
            {
                Log($"没开始放苗");
                return false;
            }

            var scheduleList = new List<Schedule>();

            foreach (var scheduleItem in scheduleDic)
            {
                var schedule = new Schedule
                {
                    JSSJ = scheduleItem.GetString("JSSJ"),
                    KSSJ = scheduleItem.GetString("KSSJ"),
                    SJYYL = scheduleItem.GetString("SJYYL"),
                    YYL = scheduleItem.GetString("YYL"),
                };

                scheduleList.Add(schedule);
            }

            BuildOrderList(scheduleList);

            return true;
        }

        private void BuildOrderList(List<Schedule> scheduleList)
        {
            var orderList = new List<Order>();


            var defaultUser = MainSession.Users.FirstOrDefault();
            foreach (var schedule in scheduleList)
            {
                Order orderWithTime = BuildOneOrder(defaultUser, Date, schedule);
                orderList.Add(orderWithTime);
            }
            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);
        }

        private Order BuildOneOrder(HuangshiLogin user, string date, Schedule schedule)
        {
            //var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            //var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            //var doctorName = MainSession.PlatformSession.GetString(Constants.DoctorName);
            var time = UnicodeConverter.EncodeOriginal($"{schedule.KSSJ}-{schedule.JSSJ}", true);

            return new Order
            {
                DeptId = user.DeptId,
                UserName = user.UserName,
                User = user,
                Birthday = user.Birthday,
                ContactPhone = user.Phone,
                Date = date,
                SFZHM = user.IdCard,
                Time = time,
                UserPhone= user.Phone,
            };
        }
    }
}
