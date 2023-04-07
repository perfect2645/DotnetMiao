using HttpProcessor.Client;
using Huangshi.common;
using Huangshi.login;
using Huangshi.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Huangshi.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(HuangshiLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(HuangshiLogin user)
        {
            try
            {
                var url = $"https://hscx.whcdc.org/vaccineServer/patientManager/getPatientList?openId=";
                var content = new MainContent(url, user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                if (code != "0" || msg != "操作完成")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信失败:code={code}, message: {msg}");
                    return;
                }
                var data = root.GetProperty("data");
                SaveUser(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data, HuangshiLogin user)
        {
            var familyMembers = JsonAnalysis.JsonToDicList(data);
            if (!familyMembers.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = familyMembers.FirstOrDefault(x => x["patientName"].NotNullString() == user.UserName);
            if (defaultUser == null)
            {
                defaultUser = familyMembers.FirstOrDefault();
            }
            var patientName = defaultUser.GetString("patientName");
            var pkid = defaultUser.GetString("pkid");
            var cardNo = defaultUser.GetString("cardNo");
            var phone = defaultUser.GetString("phone");
            var openId = defaultUser.GetString("openId");

            user.UserName = patientName;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
