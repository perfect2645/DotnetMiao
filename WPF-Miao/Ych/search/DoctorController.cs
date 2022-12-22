using Base.viewmodel.status;
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
using Utils.stringBuilder;
using Utils.timerUtil;
using Ych.appointment;
using Ych.session;

namespace Ych.search
{
    public class DoctorController : HttpClientBase
    {
        public DoctorController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetDoctorAsync()
        {
            await Task.Factory.StartNew(() => GetDoctor());
        }

        private void GetDoctor()
        {
            try
            {
                var content = new UserContent();
                content.BuildDefaultHeaders(Client);
                HttpDicResponse response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"获取医生信息失败{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var message = root.GetProperty("message").NotNullString();
                if (!"200".Equals(code) || !message.Contains("成功"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取医生信息失败:code={code}, message={message}");
                    return;
                }
                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取医生信息失败:code={code}, message={message}");
                    return;
                }
                SaveUserInfo(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取医生信息失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SaveUserInfo(JsonElement data)
        {
            var userListElement = data.GetProperty("list");
            var userList = JsonAnalysis.JsonToDicList(userListElement);
            if (!userList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取医生信息失败: userList为空");
                return;
            }

            var defaultUser = userList.FirstOrDefault();

            var defaultUserId = defaultUser.GetString("id");
            MainSession.PlatformSession.AddOrUpdate(Constants.UserId, defaultUserId);
            var defaultUserName = defaultUser.GetString("userName");
            MainSession.PlatformSession.AddOrUpdate(Constants.UserName, defaultUserName);
            MainSession.PlatformSession.AddOrUpdate("user", defaultUser);

            MainSession.PrintLogEvent.Publish(this, userList);
        }
    }
}
