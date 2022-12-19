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
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.stringBuilder;

namespace suiyang.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetUserAsync()
        {
            await Task.Factory.StartNew(() => GetUser());
        }

        private void GetUser()
        {
            try
            {
                var dept = MainSession.PlatformSession.GetString(Constants.DeptId);
                var dc = DateTimeUtil.GetTimeStamp();
                var url = $"http://www.jxy-tech.com/api/v1/myAddresss?_dc={dc}&includeItems=true&page=1&limit=10";
                var content = new SuiyangBaseContent(url);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
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
                MainSession.PrintLogEvent.Publish(this, $"GetUser异常{ex.Message}");
            }
        }

        private void SaveMiaoInfo(JsonElement data)
        {
            var userList = JsonAnalysis.JsonToDicList(data);
            MainSession.PlatformSession.AddOrUpdate("userList", userList);
            //var scheduleList = JsonAnalysis.JsonToDicList(data);
            //foreach (var schedule in scheduleList)
            //{
            //    var availableQty = schedule.GetString("availableQty").ToInt();
            //    if (availableQty > 0)
            //    {
            //        IntervalOnTime.StopInterval();
            //        PublishMiaoGet(Date);
            //        MainSession.PrintLogEvent.Publish(null, $"查到苗，date={Date}");
            //    }
            //}
        }
    }
}
