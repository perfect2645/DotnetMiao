using HttpProcessor.Client;
using Shengzhi.login;
using Shengzhi.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.RightsManagement;
using System.Text.Json;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.stringBuilder;

namespace Shengzhi.search
{
    internal class DoctorController : HttpClientBase
    {
        public DoctorController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool GetDoctors(ShengzhiLogin user)
        {
            try
            {
                var content = new DoctorContent(user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetDate - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var success = root.GetProperty("success");
                var message = root.GetProperty("message").GetString();
                if (success.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取日期信息失败: {message}");
                    return false;
                }
                if (success.GetBoolean() == false)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取日期信息失败: {message}");
                    return false;
                }

                var data = root.GetProperty("data");
                var rows = data.GetProperty("rows");
                return CheckDoctors(rows, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取日期信息异常{ex.Message}");
                return false;
            }
        }

        private bool CheckDoctors(JsonElement rows, ShengzhiLogin user)
        {
            var docs = JsonAnalysis.JsonToDicList(rows);
            if (!docs.HasItem())
            {
                Log($"获取医生信息失败");
                return false;
            }

            var targetDocId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var validDoc = docs.FirstOrDefault(d => d.GetString("DOCTOR_CODE") == targetDocId);
            if (validDoc == null)
            {
                Log($"获取医生信息失败");
                return false;
            }

            var scheduleStr = (validDoc as Dictionary<string, object>)["DOCTOR_SCHEDULE_LIST"].NotNullString();

            var scheduleList = JsonAnalysis.JsonToDicList(scheduleStr);
            if (!scheduleList.HasItem())
            {
                Log($"获取scheduleList失败");
                return false;
            }

            MainSession.PlatformSession.AddOrUpdate("scheduleList", scheduleList);

            MainSession.PrintLogEvent.Publish(this, "获得预约日期");
            return true;
        }
    }
}
