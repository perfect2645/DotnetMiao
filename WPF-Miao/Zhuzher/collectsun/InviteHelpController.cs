using HttpProcessor.Client;
using HttpProcessor.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.Common;
using Zhuzher.Post;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.collectsun
{
    public class InviteHelpController : HttpClientBase
    {
        public InviteHelpController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void InviteHelpAsync(UserProject targetUser, List<UserProject> sourceUsers)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in sourceUsers)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() =>
                    {
                        InviteHelp(targetUser, user);
                    });
                }
            });
        }

        public void InviteHelp(UserProject targetUser, UserProject sourceUser)
        {
            try
            {
                var content = new InviteHelpContent(targetUser, sourceUser);
                content.BuildDefaultHeaders(Client);
                var response = Client.PutJsonAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{sourceUser.UserName}]给[{targetUser.UserName}]助力失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").GetUInt32();

                if (code != 200)
                {
                    var msg = root.GetProperty("message").GetString();
                    MainSession.PrintLogEvent.Publish(this, $"[{sourceUser.UserName}]给[{targetUser.UserName}]助力失败: code={code}, message={msg}");
                    return;
                }

                var result = root.GetProperty("result");
                SummarizeResult(result, targetUser, sourceUser);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{sourceUser.UserName}]给[{targetUser.UserName}]助力失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SummarizeResult(JsonElement result, UserProject targetUser, UserProject sourceUser)
        {
            var msg = result.GetProperty("message").GetString();
            MainSession.PrintLogEvent.Publish(this, $"[{sourceUser.UserName}]给[{targetUser.UserName}]助力成功");
        }
    }
}
