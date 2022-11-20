using Base.viewmodel.status;
using HttpProcessor.Client;
using suiyang.common;
using suiyang.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.datetime;
using Utils.json;
using Utils.stringBuilder;

namespace suiyang.search
{
    internal class GetMiaoController : HttpClientBase
    {
        public GetMiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetMiaoAsync()
        {
            Task.Factory.StartNew(() => GetMiao());
        }

        private void GetMiao()
        {
            try
            {
                var url = $"http://www.jxy-tech.com/api/v1/locations/1/canbeappointed/businesses/F/availableDates?_dc={DateTimeUtil.GetTimeStamp()}";
                var content = new SuiyangBaseContent(url);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetMiao - {response?.Message},请检查参数");
                }
                var success = response.JsonBody.RootElement.GetProperty("success").NotNullString().ToBool();
                if (success)
                {
                    var data = response.JsonBody.RootElement.GetProperty("data");
                    SaveMiaoInfo(data);
                    MainSession.SetStatus(MiaoProgress.ReadyForSearch);
                    return;
                }
                MainSession.SetStatus(MiaoProgress.Init);
                var msg = response.JsonBody.RootElement.GetProperty("message").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{msg}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetFamily异常{ex.Message}");
            }
        }

        private void SaveMiaoInfo(JsonElement data)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(data);
            foreach (var schedule in scheduleList)
            {
                //var familyId = member.GetString(Constants.FamilyID);
                //MainSession.UserSession.AddUser(familyId, member);
            }
        }

    }
}
