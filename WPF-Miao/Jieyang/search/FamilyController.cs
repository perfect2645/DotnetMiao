using Base.viewmodel.status;
using jieyang.common;
using jieyang.session;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace jieyang.search
{
    public class FamilyController : HttpClientBase
    {
        public FamilyController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetFamilyAsync(string userId)
        {
            await Task.Factory.StartNew(() => GetFamily(userId));
        }

        private void GetFamily(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                MainSession.PrintLogEvent.Publish(this, $"GetFamily failed. userid is empty");
                MainSession.SetStatus(MiaoProgress.Init);
                return;
            }

            try
            {
                var url = $"https://ctmingyi.com:18082/api/family/getFamilyPage?userID={userId}";
                var content = new jieyangBaseContent(url);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetFamily失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var success = response.JsonBody.RootElement.GetProperty("success").NotNullString().ToBool();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{msg}");
                if ("0".Equals(code) && success)
                {
                    var obj = response.JsonBody.RootElement.GetProperty("obj");
                    var familyInfo = obj.GetProperty("records");
                    SaveFamilyInfo(familyInfo);
                    MainSession.SetStatus(MiaoProgress.ReadyForSearch);
                    return;
                }
                MainSession.SetStatus(MiaoProgress.Init);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetFamily异常{ex.Message}");
            }
        }

        private void SaveFamilyInfo(JsonElement familyInfo)
        {
            var familyMembers = JsonAnalysis.JsonToDicList(familyInfo);
            foreach(var member in familyMembers)
            {
                MainSession.PrintLogEvent.Publish(this, member);
            }
        }
    }
}
