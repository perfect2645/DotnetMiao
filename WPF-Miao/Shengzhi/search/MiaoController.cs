using Base.viewmodel.status;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Shengzhi.appointment;
using Shengzhi.common;
using Shengzhi.login;
using Shengzhi.session;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;
using System.Text;

namespace Shengzhi.search
{
    internal class MiaoController : HttpClientBase
    {

        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync(ShengzhiLogin user, Dictionary<string, object> schedule)
        {
            Task.Factory.StartNew(() => SearchMiao(user, schedule));
        }

        public bool SearchMiao(ShengzhiLogin user, Dictionary<string, object> schedule)
        {
            try
            {
                Date = schedule.GetString("CLINIC_DATE");
                var content = new MiaoContent(user, schedule);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败 - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var success = root.GetProperty("success");
                var message = root.GetProperty("message").GetString();
                if (success.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: {message}");
                    return false;
                }
                if (success.GetBoolean() == false)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: {message}");
                    return false;
                }

                var data = root.GetProperty("data");
                var rows = data.GetProperty("rows");
                return CheckMiao(rows, user, schedule);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }


        private bool CheckMiao(JsonElement data, ShengzhiLogin user, Dictionary<string, object> schedule)
        {
            var timeList = JsonAnalysis.JsonToDicList(data);
            if (!timeList.HasItem())
            {
                Log($"查苗失败失败");
                return false;
            }

            BuildOrderList(timeList, user, schedule);

            MainSession.PrintLogEvent.Publish(this, "查到苗");
            return true;
        }

        private void BuildOrderList(List<Dictionary<string, object>> timeList, ShengzhiLogin user, Dictionary<string, object> schedule)
        {
            var orderList = new List<Order>();

            foreach (var timeInfo in timeList)
            {
                Order orderWithTime = BuildOneOrder(user, schedule, timeInfo);
                orderList.Add(orderWithTime);
            }

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);
        }

        private Order BuildOneOrder(ShengzhiLogin user, Dictionary<string, object> schedule, Dictionary<string, object> timeInfo)
        {
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var doctorName = MainSession.PlatformSession.GetString(Constants.DoctorName);

            var hid = timeInfo.GetString("HID");
            var duration = schedule.GetString("CLINIC_DURATION");
            var hidParts = new StringBuilder();
            var hidPart1 = UnicodeConverter.EncodeOriginal(hid.Replace(",", ""), true);
            var hidPart2 = UnicodeConverter.EncodeOriginal($"/{duration} ", true);
            hidParts.Append(hidPart1);
            hidParts.Append(",");
            hidParts.Append(hidPart2);
            hidParts.Append(timeInfo.GetString("HB_TIME"));

            var hidEncode = hidParts.ToString();
            return new Order
            {
                Amount = "0.00",
                AppointScore = user.AppointSource,
                AppUuid = user.AppUuid,
                ChannelId = user.ChannelId,
                DeptCode = deptId,
                GroupCode = user.GroupCode,
                HidEncode = hidEncode,
                ImeiId = user.ImeiId,
                IsCreateCard = "0",
                MarkDescEncode = UnicodeConverter.EncodeOriginal(doctorName, true),
                PatientId = user.PatientId,
            };
        }
    }
}
