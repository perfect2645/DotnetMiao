using HttpProcessor.Client;
using Longchi.common;
using Longchi.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Longchi.search
{
    internal class ConfirmLoginController : HttpClientBase
    {
        public ConfirmLoginController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task ConfirmLoginAsync(LongchiLogin user)
        {
            await Task.Factory.StartNew(() => ConfirmLogin(user));
        }

        private void ConfirmLogin(LongchiLogin user)
        {
            var content = new ConfirmLoginContent(user.Cookie);
            content.BuildDefaultHeaders(Client);
            var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
            if (response?.Body == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"ConfirmLogin - {response?.Message},请检查参数");
                return;
            }
            var root = response.JsonBody.RootElement;
            var code = root.GetProperty("code").NotNullString();
            var msg = root.GetProperty("msg").NotNullString();
            MainSession.PrintLogEvent.Publish(this, $"获取用户信息msg:{msg}");
            if (code != "1")
            {
                MainSession.PrintLogEvent.Publish(this, $"验证登录失败:{msg}");
                return;
            }
            var data = root.GetProperty("data");
            MainSession.PrintLogEvent.Publish(this, $"验证登录:{msg}, data={data.ToString()}");
        }

        private void SaveUser(JsonElement data, LongchiLogin user)
        {
            var familyMembers = JsonAnalysis.JsonToDicList(data);
            if (!familyMembers.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = familyMembers.FirstOrDefault(x => x["姓名"].NotNullString() == user.UserName);
            var familyId = defaultUser.GetString("id");
            var userName = defaultUser.GetString("姓名");
            var userId = defaultUser.GetString("预约者ID");
            var userAddr = defaultUser.GetString("居住地址");
            var userSuoshu = defaultUser.GetString("所属单位");
            var userCode = defaultUser.GetString("身份证号");

            user.FamilyId = familyId;
            user.UserId = userId;
            user.UserName = userName;
            user.YuyueUserAdd = userAddr;
            user.YuyueUserSuoshu = userSuoshu;
            user.UserCode = userCode;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
