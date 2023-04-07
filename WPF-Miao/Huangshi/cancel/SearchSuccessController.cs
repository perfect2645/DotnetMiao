using HttpProcessor.Client;
using Huangshi.common;
using Huangshi.login;
using Huangshi.session;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Huangshi.cancel
{
    internal class SearchSuccessController : HttpClientBase
    {
        public SearchSuccessController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task SearchHistoryAsync(HuangshiLogin user)
        {
            return Task.Factory.StartNew(() => SearchHistory(user));
        }

        private void SearchHistory(HuangshiLogin user)
        {
            try
            {
                var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
                var url = $"https://hscx.whcdc.org/vaccineServer/RegApiManage/regRecord?patientId={deptId}&status=";
                var content = new MainContent(url, user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchHistory - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                if (code != "0" || msg != "操作完成")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取预约历史失败:code={code}, message: {msg}");
                    return;
                }
                var data = root.GetProperty("data");
                SaveHistory(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取预约历史异常{ex.Message}");
            }
        }

        private void SaveHistory(JsonElement data, HuangshiLogin user)
        {
            var histories = JsonAnalysis.JsonToDicList(data);
            if (!histories.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取预约历史失败");
                return;
            }

            var validHistories = histories.Where(h => h["status"].NotNullString() == "-1").ToList();
            if (!validHistories.HasItem())
            {
                return;
            }

            var historyList = new List<History>();
            foreach (var his in validHistories)
            {
                var date = his.GetString("visitDate");
                var time = his.GetString("visitTime");
                var history = new History
                {
                    registrationId = his.GetString("registrationId"),
                    cardNo = his.GetString("cardNo"),
                    patientName = his.GetString("patientName"),
                    visitDate = date,
                    visitTime = time,
                    Key = $"{date} | {time}",
                };

                historyList.Add(history);
            }

            MainSession.HistoryList.AddRange(historyList);
        }
    }
}
