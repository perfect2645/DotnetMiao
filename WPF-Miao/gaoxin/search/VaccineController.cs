using gaoxin.appointment;
using gaoxin.common;
using gaoxin.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils.stringBuilder;

namespace gaoxin.search
{
    internal class VaccineController : GaoxinControllerBase
    {
        public Order OrderResult { get; private set; }
        public VaccineController(HttpClient httpClient) : base(httpClient)
        {
            ProcessAction = new Action<GaoxinContent>(GetVaccineOrder);
        }

        private void GetVaccineOrder(GaoxinContent content)
        {
            var vaccineContent = content as VaccineContent;
            if (vaccineContent == null)
            {
                return;
            }
            HttpDicResponse response = PostStringDecodeAsync(vaccineContent, Decode).Result;
            var resultValue = CheckGetResultValue(response);
            if (resultValue.ValueKind == JsonValueKind.Null)
            {
                return;
            }

            if (resultValue.ValueKind == JsonValueKind.Undefined)
            {
                return;
            }

            if (resultValue.ValueKind == JsonValueKind.String)
            {
                var result = resultValue.NotNullString();
                var message = $"{result} - Token: {vaccineContent.UserInfo.Token}";
                MainSession.PrintLogEvent.Publish(this, message);
                return;
            }

            SaveVaccine(resultValue, vaccineContent.UserInfo);
        }

        private void SaveVaccine(JsonElement resultValue, UserInfo userInfo)
        {
            var slVaccineDispark = resultValue.GetProperty("slVaccineDispark");
            var commId = slVaccineDispark.GetProperty("commId").NotNullString();

            var vaccineInfo = resultValue.GetProperty("vaccineInfo");
            var vaccineId = vaccineInfo.GetProperty("id").NotNullString();
            var vaccinePrice = vaccineInfo.GetProperty("price").GetDouble();

            var order = new Order()
            {
                daid = userInfo.daid,
                disparkId = MainSession.DisparkId,
                jgcommid = "10",
                jgid = commId,
                UserName = userInfo.UserName,
                yyjg = (int)vaccinePrice,
                yyymid = vaccineId,
            };

            OrderResult = order;
        }
    }
}
