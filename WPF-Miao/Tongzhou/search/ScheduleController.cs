using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tongzhou.common;
using Tongzhou.login;
using Tongzhou.session;
using Utils;
using Utils.json;

namespace Tongzhou.search
{
    internal class ScheduleController : HttpClientBase
    {
        public ScheduleController(HttpClient httpClient) : base(httpClient)
        {
        }
        public void GetScheduleAsync(TongzhouLogin user)
        {
            Task.Factory.StartNew(() => GetSchedule(user));
        }

        private void GetSchedule(TongzhouLogin user)
        {
            try
            {
                var content = new ScheduleContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.ContentStr == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }

                var dicResultDe = Encrypt.Decrypt22(response?.ContentStr);
                var dicResult = JsonAnalysis.JsonToDic(dicResultDe);

                CheckSaveUser(dicResult, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private bool CheckSaveUser(Dictionary<string, object> dicResult, TongzhouLogin user)
        {
            var code = dicResult.GetString("code").ToInt();
            if (code != 200)
            {
                var msg = dicResult.GetString("msg");
                MainSession.PrintLogEvent.Publish(this, $"Get Userinfo Failed.code={code}, msg={msg}");
                return false;
            }

            return true;
        }
    }
}
