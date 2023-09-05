using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using HttpProcessor.JsonFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tongzhou.common;
using Tongzhou.login;
using Tongzhou.session;

namespace Tongzhou.search
{
    internal class TimestampController : HttpClientBase
    {
        public TimestampController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<bool> GetGetHeaderTimestampAsync(TongzhouLogin user)
        {
            return await Task.Factory.StartNew(() => GetHeaderTimestamp(user));
        }

        public bool GetHeaderTimestamp(TongzhouLogin user)
        {
            try
            {
                var url = "https://weixin.ngarihealth.com/weixin//api/timestamp";
                var content = new TongzhouContent(url, user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetHeaderTimestampAsync - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetStringValue("code");
                if (code != "200")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取HeaderTimestamp失败: code = {code}");
                    return false;
                }
                BuildTimestamp(root, user);

                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取HeaderTimestamp失败{ex.Message}");
                return false;
            }
        }

        private void BuildTimestamp(JsonElement root, TongzhouLogin user)
        {
            user.Timestamp = root.GetStringValue("body");
            user.Uuid = root.GetStringValue("uuid");
            user.XCaNonce = $"{user.Uuid}{user.Timestamp}{MainSession.LocalTimeOffset}";
        }
    }
}
