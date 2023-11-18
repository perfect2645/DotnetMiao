using HttpProcessor.Client;
using Newtonsoft.Json;
using Rika.appointment;
using Rika.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;

namespace Rika.search
{
    public class DateController : HttpClientBase
    {
        public DateController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<bool> SearchByDateAsync(int index)
        {
            return Task.Factory.StartNew(() => SearchByDate(index));
        }

        public bool SearchByDate(int index)
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new DateContent(defaultUser, index);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchByDate - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                if (root.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchByDate失败: root is empty");
                    return false;
                }
                var resultDate = root.GetProperty("rq").GetString();

                var data = root.GetProperty("data");

                return CheckSaveResource(resultDate, data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveResource(string resultDate, JsonElement dataElement)
        {
            var vaccineDayList = JsonAnalysis.JsonToDicList(dataElement);
            if (!vaccineDayList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            return true;
        }
    }
}
