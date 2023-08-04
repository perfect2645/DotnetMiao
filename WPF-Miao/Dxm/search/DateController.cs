using Dxm.appointment;
using Dxm.session;
using HttpProcessor.Client;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.number;
using Utils.Rdom;
using Utils.stringBuilder;

namespace Dxm.search
{
    internal class DateController : HttpClientBase
    {
        public string Date { get; set; }
        public DateController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchDate(string date)
        {
            Date = date;
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var vaccineId = MainSession.PlatformSession.GetString(Constants.DeptId);
                var content = new DateContent(defaultUser, vaccineId);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDate - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDate: code={code}");
                    return false;
                }

                var result = root.GetProperty("result");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDate: results is empty");
                    return false;
                }

                var dateDetails = result.GetProperty("details");
                if (dateDetails.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDate: dateDetails is empty");
                    return false;
                }

                return CheckSaveDate(dateDetails);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到Vaccine - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveDate(JsonElement dateListData)
        {
            var dateList = JsonAnalysis.JsonToDicList(dateListData);

            if (!dateList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"还没开始");
                return false;
            }

            var validDateList = dateList.Where(d => d.GetString("status") == "0").ToList();
            if (!validDateList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用日期");
                return false;
            }

            var matchedDate = validDateList.FirstOrDefault(d => d.GetString("makeAnAppointment") == Date);
            if (matchedDate == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"没有指定日期，随机选择");
                matchedDate = validDateList.GetRandomMember();
            }

            Date = matchedDate.GetString("makeAnAppointment");

            return true;
        }
    }
}