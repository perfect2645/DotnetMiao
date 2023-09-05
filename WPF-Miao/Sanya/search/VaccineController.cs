using Sanya.appointment;
using Sanya.session;
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

namespace Sanya.search
{
    internal class VaccineController : HttpClientBase
    {
        public string Date { get; set; }
        public VaccineController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchVaccine()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new VaccineContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchDepartment - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var message = root.GetProperty("message").GetString();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败: code={code}, message = {message}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败: results is empty");
                    return false;
                }

                return CheckSaveVaccine(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到Vaccine - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveVaccine(JsonElement classList)
        {
            var vaccineList = JsonAnalysis.JsonToDicList(classList);

            if (!vaccineList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"还没开始");
                return false;
            }

            var vaccineName = MainSession.PlatformSession.GetString(Constants.VaccineName);
            var targetVaccine = vaccineList.FirstOrDefault(x => x.GetString("serviceName").Contains(vaccineName));
            if (targetVaccine == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"没找到对应的疫苗（{vaccineName}），自动选择9价");
                targetVaccine = vaccineList.FirstOrDefault(x => x.GetString("serviceName").Contains("九价"));
            }
            if (targetVaccine == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"没找到对应的疫苗，也没九价");
                return false;
            }

            var serviceId = targetVaccine.GetString("serviceId");
            if (string.IsNullOrEmpty(serviceId))
            {
                MainSession.PrintLogEvent.Publish(this, $"serviceId is empty, 没开始");
                return false;
            }


            MainSession.PlatformSession[Constants.DeptId] = serviceId;

            return true;
        }
    }
}