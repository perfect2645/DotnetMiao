using Puzhou.appointment;
using Puzhou.session;
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

namespace Puzhou.search
{
    internal class VaccineController : HttpClientBase
    {
        public string Date { get; set; }

        public VaccineController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchVaccine(string date)
        {
            try
            {
                Date = date;
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new VaccineContent(defaultUser, date);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine: code={code}");
                    return false;
                }

                var result = root.GetProperty("result");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine: results is empty");
                    return false;
                }

                var vaccineListData = result.GetProperty("list");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine: vaccineListData is empty");
                    return false;
                }

                return CheckSaveVaccine(vaccineListData);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到Vaccine - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveVaccine(JsonElement vaccineListData)
        {
            var vaccineList = JsonAnalysis.JsonToDicList(vaccineListData);

            if (!vaccineList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"{Date} 还没开始");
                return false;
            }

            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var targetVaccine = vaccineList.FirstOrDefault(x => x.GetString("name").Contains(deptName));
            if (targetVaccine == null)
            {
                targetVaccine = vaccineList.FirstOrDefault(x => x.GetString("name").Contains("九价"));
            }

            if (targetVaccine == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{Date} Not match the expect vaccine [{deptName}] ");
                return false;
            }

            var vaccineId = targetVaccine.GetString("vaccineInfoId");
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, vaccineId);
            MainSession.SetStatus(Base.viewmodel.status.MiaoProgress.MiaoGet);

            return true;
        }
    }
}