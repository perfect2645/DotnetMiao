using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using gaoxin.common;
using gaoxin.session;
using Utils.stringBuilder;
using System.Text.Json;
using Utils;
using Utils.json;

namespace gaoxin.search
{
    internal class UserController : GaoxinControllerBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
            ProcessAction = new Action<GaoxinContent>(GetUser);
        }

        private void GetUser(GaoxinContent content)
        {
            HttpDicResponse response = PostStringDecodeAsync(content, Decode).Result;
            var resultValue = CheckGetResultValue(response);
            if (resultValue.ValueKind == JsonValueKind.Null)
            {
                return;
            }

            SaveUser(resultValue);
        }

        private void SaveUser(JsonElement resultValue)
        {
            var slVaccineDispark = resultValue.GetProperty("slVaccineDispark");
            var vaccineId = slVaccineDispark.GetProperty("id").NotNullString();
            MainSession.PlatformSession.AddOrUpdate("yyymid", vaccineId);

            var vaccinePrice = slVaccineDispark.GetProperty("price").GetDouble();
            MainSession.PlatformSession.AddOrUpdate("yyjg", vaccinePrice);

            var slVaccineSlUserinfos = resultValue.GetProperty("slVaccineSlUserinfos");
            var userList = JsonAnalysis.JsonToDicList(slVaccineSlUserinfos);

            var targetUser = userList.LastOrDefault();
            var userId = targetUser.GetString("id");
            var userName = targetUser.GetString("xingm");
            MainSession.PlatformSession.AddOrUpdate(Constants.UserId, userId);
            MainSession.PlatformSession.AddOrUpdate(Constants.UserName, userName);

            MainSession.PrintLogEvent.Publish(this, targetUser);
        }
    }
}
