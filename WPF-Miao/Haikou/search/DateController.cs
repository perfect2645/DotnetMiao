using Haikou.login;
using Haikou.session;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Haikou.search
{
    internal class DateController : HttpClientBase
    {
        public DateController(HttpClient httpClient) : base(httpClient)
        {
        }

        public List<string> GetDateList()
        {
            var result = new List<string>();
            try
            {
                var user = MainSession.Users.FirstOrDefault();
                var content = new DateContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetDateList - {response?.Message},请检查参数");
                    return result;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var msg = root.GetProperty("msg").GetString();
                if (code != 0 || msg != "success")
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetDate失败:{user.UserName} code={code}, msg={msg}");
                    return result;
                }

                var data = root.GetProperty("healthCardList");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetDate失败: results is empty");
                    return result;
                }
                return GetTargetDate(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetDate异常{ex.Message}");
                return result;
            }
        }

        private List<string> GetTargetDate(JsonElement data, HaikouLogin user)
        {
            var result = new List<string>();
            var userList = JsonAnalysis.JsonToDicList(data);
            if (!userList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"GetDate失败");
                return result;
            }

            return result;
        }
    }
}
