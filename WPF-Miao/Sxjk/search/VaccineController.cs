using Sxjk.appointment;
using Sxjk.session;
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
using Sxjk.common;
using Sxjk.login;

namespace Sxjk.search
{
    internal class VaccineController : SxjkController
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

                var resultDic = CheckResult(root);
                if (!resultDic.HasItem())
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败 - {response?.Message}");
                    return false;
                }

                var success = resultDic.GetString("success");
                if ("False".Equals(success, StringComparison.OrdinalIgnoreCase))
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败 - {resultDic.GetString("message")}");
                    return false;
                }

                var data = resultDic.GetString("data");
                return CheckSaveVaccine(data, defaultUser);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到Vaccine - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveVaccine(string data, SxjkLogin user)
        {
            var vaccineDataList = JsonAnalysis.JsonToDicList(data);

            if (!vaccineDataList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没查到vaccine信息");
                return false;
            }

            var defaultVaccine = vaccineDataList.LastOrDefault(x => x.GetString("vaccine_producer").Contains("9价"));

            user.PriceId = "3902";
            MainSession.PlatformSession.AddOrUpdate("priceId", "3902");

            if (defaultVaccine == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"没查到vaccine_producer， priceId 设为默认值 3902");
                return true;
            }

            MainSession.PlatformSession.AddOrUpdate("priceId", defaultVaccine.GetString("priceId"));
            return true;
        }
    }
}