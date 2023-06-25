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

        public bool SearchVaccine()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new VaccineContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var success = root.GetProperty("success").GetBoolean();
                var msg = root.GetProperty("msg").GetString();
                if (!success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败: success={success}, msg = {msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败: results is empty");
                    return false;
                }

                var list = data.GetProperty("list");

                return CheckSaveVaccine(list);
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
                MainSession.PrintLogEvent.Publish(this, $"还没开始");
                return false;
            }

            var defaultVaccine = vaccineList.LastOrDefault(x => x.GetString("last_num_count").ToInt() > 0);
            if (defaultVaccine == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"抢光了");
                return false;
            }

            Date = defaultVaccine.GetString("date");

            MainSession.SetStatus(Base.viewmodel.status.MiaoProgress.MiaoGet);

            return true;
        }
    }
}