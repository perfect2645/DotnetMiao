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
                var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
                var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
                var url = $"https://ldsq.ldrmyy120.com/rest/v1/api/examine/shift_time_for_vaccine/{deptId}/?date={date}&hospital={hosId}";
                var content = new JikongContent(url, user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败 - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var detail = root.GetProperty("shift_times_obj");
                if (detail.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: results is empty");
                    return false;
                }
                return CheckTime(detail, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }


        private bool CheckTime(JsonElement data, JikongLogin user)
        {
            var times = JsonAnalysis.JsonToDicList(data);
            if (!times.HasItem())
            {
                Log($"查苗失败失败");
                return false;
            }

            var validTimes = times.Where(t => t["stay_num"].NotNullString().ToInt() > 0);
            if (!validTimes.HasItem())
            {
                Log("没有可用时间");
                return false;
            }

            var timeIdList = validTimes.Select(d => d["id"].NotNullString()).ToList();

            MainSession.PlatformSession.AddOrUpdate("timeIdList", timeIdList);

            BuildOrderList(timeIdList);

            MainSession.PrintLogEvent.Publish(this, "获得预约日期");
            return true;
        }

        private void BuildOrderList(List<string> timeIdList)
        {
            var orderList = new List<Order>();

            foreach (var user in MainSession.Users)
            {
                var userName = user.UserName;
                foreach (var timeId in timeIdList)
                {
                    Order orderWithTime = BuildOneOrder(user, Date, timeId);
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

        private Order BuildOneOrder(JikongLogin user, string date, string timeId)
        {
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            return new Order
            {
                Address = user.Address,
                DutyTimeId = timeId,
                HosipitalId = hospitalId,
                InoculateTimes = user.InoculateTimes,
                SeeDate = date,
                UserId = user.UserId,
                UserName = user.UserName,
                User = user,
                VaccineId = deptId
            };
        }
    }
}
