using HttpProcessor.Client;
using Huangdai.common;
using Huangdai.login;
using Huangdai.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Huangdai.login
{
    internal class OpenIdController : HttpClientBase
    {
        public OpenIdController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetOpenIdAsync(HuangdaiLogin user)
        {
            await Task.Factory.StartNew(() => GetOpenId(user));
        }

        private void GetOpenId(HuangdaiLogin user)
        {
            try
            {
                var url = $"https://hscx.whcdc.org/vaccineServer/wechat/getOpenIdByCode?code={user.Code}";
                var content = new HuangdaiContent(url, user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetOpenId - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                if (code != "0" || msg != "操作完成")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取OpenId失败:code={code}, message: {msg}");
                    return;
                }
                var data = root.GetProperty("data");
                SaveOpenId(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取OpenId异常{ex.Message}");
            }
        }

        private void SaveOpenId(JsonElement data, HuangdaiLogin user)
        {
            var familyMembers = JsonAnalysis.JsonToDicList(data);
            if (!familyMembers.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取OpenId失败");
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

            user.UserId = pkid;
            user.UserName = patientName;
            user.IdCard = cardNo;
            user.Phone = phone;
            user.OpenId = openId;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
