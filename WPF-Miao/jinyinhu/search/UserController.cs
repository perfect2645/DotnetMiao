using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using jinyinhu.common;
using jinyinhu.session;
using Utils.stringBuilder;
using System.Text.Json;
using Utils;
using Utils.json;

namespace jinyinhu.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync()
        {
            Task.Factory.StartNew(() => GetUser());
        }

        private void GetUser()
        {
            try
            {
                var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
                var url = $"http://101.34.141.250:9653/api/front/sgUser/getUserByTpye/{deptId}";
                var content = new JinyinhuContent(url);
                content.BuildDefaultHeaders(Client);
                HttpDicResponse response = GetStringAsync(content).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"获取用户信息失败{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var result = root.GetProperty("result").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息-{result}");
                if ("Success".Equals(result))
                {
                    var userMap = root.GetProperty("map");
                    SaveUserInfo(userMap);
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SaveUserInfo(JsonElement userMap)
        {
            var defaultUserId = userMap.GetProperty("defaultPatientId").NotNullString();
            if (string.IsNullOrEmpty(defaultUserId))
            {
                MainSession.PrintLogEvent.Publish(this, $"默认用户Id为空，请检查注册信息");
                return;
            }
            MainSession.PlatformSession.AddOrUpdate(Constants.UserId, defaultUserId);

            var userListElement = userMap.GetProperty("list");
            var userList = JsonAnalysis.JsonToDicList(userListElement);
            if (!userList.HasItem())
            {
                Log($"用户列表为空，请检查注册信息");
                return;
            }

            var defaultUserName = userList.FirstOrDefault().GetString("name");
            MainSession.PlatformSession.AddOrUpdate(Constants.UserName, defaultUserName);
        }
    }
}
