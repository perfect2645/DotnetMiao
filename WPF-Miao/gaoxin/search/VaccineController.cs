using gaoxin.appointment;
using gaoxin.common;
using gaoxin.session;
using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace gaoxin.search
{
    internal class VaccineController : GaoxinControllerBase
    {
        public VaccineController(HttpClient httpClient) : base(httpClient)
        {
            ProcessAction = new Action<GaoxinContent>(GetVaccine);
        }

        private void GetVaccine(GaoxinContent content)
        {
            HttpDicResponse response = PostStringDecodeAsync(content, Decode).Result;
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
                var message = $"{result} - Token: {content.LoginInfo.Token}";
                MainSession.PrintLogEvent.Publish(this, message);
                return;
            }

            SaveVaccine(resultValue, content.LoginInfo);
        }

        private void SaveVaccine(JsonElement resultValue, GaoxinLogin loginInfo)
        {

            var slVaccineDispark = resultValue.GetProperty("slVaccineDispark");
            var vaccineId = slVaccineDispark.GetProperty("id").NotNullString();
            var vaccinePrice = slVaccineDispark.GetProperty("price").GetDouble();

            var slVaccineSlUserinfos = resultValue.GetProperty("slVaccineSlUserinfos");
            var userList = JsonAnalysis.JsonToDicList(slVaccineSlUserinfos);
            var targetUser = userList.LastOrDefault();
            var userId = targetUser.GetString("id");
            var userName = targetUser.GetString("xingm");

            var userInfo = new UserInfo();
            userInfo.yyymid = vaccineId;
            userInfo.yyjg = (int)vaccinePrice;
            userInfo.daid = userId;
            userInfo.UserName = userName;
            userInfo.OrderToken = loginInfo.OrderToken;

            MainSession.UserDic.AddOrUpdate(userId, userInfo);
            BuildOrder(userInfo);

            MainSession.PrintLogEvent.Publish(this, targetUser);
        }

        private void BuildOrder(UserInfo userInfo)
        {
            var order = new Order()
            {
                daid = userInfo.daid,
                UserName = userInfo.UserName,
                disparkId = MainSession.DisparkId,
                jgcommid = "10",
                jgid = "4",
                yyjg = userInfo.yyjg,
                yysj = "2022-12-10",
                yysjd = "08:30-16:20",
                yyymid = userInfo.yyymid,
                OrderToken = userInfo.OrderToken
            };

            MainSession.OrderDic.AddOrUpdate(userInfo.daid, order);
        }
    }
}
