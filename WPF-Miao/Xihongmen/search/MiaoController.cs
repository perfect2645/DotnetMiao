using Base.viewmodel.status;
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
using Xihongmen.appointment;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class MiaoController : HttpClientBase
    {
        private MiaoContent content;

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
            BuildContent();
        }

        public void GetMiaoAsync(string date)
        {
            Task.Factory.StartNew(() => GetMiao(date));
        }

        internal void BuildContent()
        {
            content = new MiaoContent();
            BuildClientHeaders(content);
        }

        public void GetMiao(string date)
        {
            try
            {
                content.BuildContent(date);
                var response = PostStringAsync(content, ContentType.String, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetMiao失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"GetMiao - {msg}");
                if ("200".Equals(code) && "OK".Equals(msg))
                {
                    var data = response.JsonBody.RootElement.GetProperty("data");
                    SaveMiao(data, date);
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetMiao异常{ex.Message}");
            }
        }

        private void SaveMiao(JsonElement data, string date)
        {
            var timeTypeNode = data.GetProperty("timeTypeList");
            var timeTypeList = JsonAnalysis.JsonToDicList(timeTypeNode);
            if (!timeTypeList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"GetMiao信息失败");
                return;
            }

            var timeIdList = timeTypeList.Select(t => t["id"].NotNullString()).ToList();
            //var testT = DateTimeUtil.GetTimeStamp(DateTime.Today.AddDays(4).AddHours(8));

            BuildOrderList(timeIdList, date);
        }


        private void BuildOrderList(List<string> timeIdList, string date)
        {
            var orderList = new List<Order>();

            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var deptNameEncode = UnicodeConverter.Encode(deptName, true);
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            var userInfo = MainSession.Users.FirstOrDefault();
            var userId = userInfo.UserId;
            var userName = userInfo.UserName;
            var userNameEncode = UnicodeConverter.Encode(userName, true);

            foreach (var timeId in timeIdList)
            {
                orderList.Add(new Order
                {
                    TypeId = deptId,
                    Date = date,
                    TypeTitle = deptNameEncode,
                    UserId = userId,
                    UserName = userName,
                    UserNameEncode = userNameEncode,
                    TimeType = timeId,
                });
            }

            orderList = orderList.DisorderItems();

            var appointEventArgs = new ScheduleEventArgs
            {
                OrderList = orderList
            };

            if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.MiaoGet)
            {
                return;
            }
            MainSession.ScheduleEvent.Publish(null, appointEventArgs);
            MainSession.SetStatus(MiaoProgress.MiaoGet);
        }
    }
}
