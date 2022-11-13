using Base.viewmodel.status;
using chutian.common;
using chutian.session;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace chutian.login
{
    internal class LoginController : HttpClientBase
    {
        public LoginController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task LoginAsync(string userPhone, string userPassword)
        {
            await Task.Factory.StartNew(() => Login(userPhone, userPassword));
        }

        public void Login(string userPhone, string userPassword)
        {
            try
            {
                var url = $"https://ctmingyi.com:18082/api/user/checkLogin?phone={userPhone}&password={userPassword}&imei=&isDoctor=false";
                var content = new ChutianBaseContent(url);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"登录失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var success = response.JsonBody.RootElement.GetProperty("success").NotNullString().ToBool();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"{msg}");
                if ("0".Equals(code) && success)
                {
                    var userInfo = response.JsonBody.RootElement.GetProperty("obj");
                    SaveUserInfo(userInfo);
                    MainSession.SetStatus(MiaoProgress.ReadyForSearch);
                    return;
                }
                MainSession.SetStatus(MiaoProgress.Init);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"登录异常{ex.Message}");
            }
        }

        private void SaveUserInfo(JsonElement userInfo)
        {
            var user = userInfo.GetProperty("user");
            if (user.ValueKind == JsonValueKind.Null)
            {
                user = userInfo.GetProperty("vipUser");
            }
            var token = userInfo.GetProperty("token").NotNullString();
            var dicResult = JsonAnalysis.JsonToDic(user);
            dicResult.AddOrUpdate("token", token);
            var userPhone = dicResult[Constants.UserPhone].NotNullString();
            MainSession.UserSession.AddOrUpdate(userPhone, dicResult);
            MainSession.PrintLogEvent.Publish(this, dicResult);
        }
    }
}
