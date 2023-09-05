using Yongding.appointment;
using Yongding.login;
using Yongding.session;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;
using Newtonsoft.Json;
using Utils.stringBuilder;

namespace Yongding.search
{
    internal class MiaoController : HttpClientBase
    {

        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchMiao(Order order)
        {
            try
            {
                var content = new MiaoContent(order);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var message = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: code={code}, message={message}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: data is empty");
                    return false;
                }

                return SaveTime(data, order);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool SaveTime(JsonElement dataElement, Order order)
        {
            var timeList = JsonAnalysis.JsonToDicList(dataElement);
            if (!timeList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"time list is empty");
                return false;
            }

            var defauleTime = timeList.FirstOrDefault().GetString("no");
            order.No = defauleTime ?? "1";

            return true;
        }
    }
}
