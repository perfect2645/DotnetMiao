using HttpProcessor.Client;
using Longchi.common;
using Longchi.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Longchi.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetUserAsync(LongchiLogin user)
        {
            await Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(LongchiLogin user)
        {
            var url = $"http://hpv_ym.zzytrj.net:15003/api/yuyue.php?cmd=get_user_list";
            var content = new LongchiContent(url, user.Cookie);
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
            MainSession.PrintLogEvent.Publish(this, $"获取用户信息msg:{msg}");
            if (code != "1")
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败:{msg}");
                return;
            }
            var data = root.GetProperty("data");
            SaveUser(data, user);
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

            if (user.UserName == "李雅婷")
            {
                user.UserCode = "350602200104164000";
            }

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
