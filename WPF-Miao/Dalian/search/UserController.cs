using Dalian.common;
using Dalian.login;
using Dalian.session;
using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Dalian.search
{
    internal class UserController : DalianController
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetUserAsync(DalianLogin user)
        {
            return Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(DalianLogin user)
        {
            try
            {
                var content = new UserContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }

                SaveSessionTime(response, user);

                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("resCode").GetInt32();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: code={code}");
                    return;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return;
                }

                var userList = data.GetProperty("list");

                SaveUser(userList, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data, DalianLogin user)
        {
            var userInfoList = JsonAnalysis.JsonToDicList(data);
            if (!userInfoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }


            var targetUser = userInfoList.FirstOrDefault(x => x.GetString("patientName") == user.UserName);
            if (targetUser == null)
            {
                targetUser = userInfoList.FirstOrDefault();
            }

            var userName = targetUser.GetString("patientName");
            var cardType = targetUser.GetString("idCardType");
            var cardNo = targetUser.GetString("idCardNo");
            var phone = targetUser.GetString("phone");

            user.UserName = userName;
            user.CardType = cardType;
            user.CardNo = cardNo;
            user.Phone = phone;

            MainSession.PrintLogEvent.Publish(this, targetUser);
        }
    }
}
