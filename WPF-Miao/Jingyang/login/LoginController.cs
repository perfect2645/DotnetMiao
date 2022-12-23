using Base.viewmodel.status;
using HttpProcessor.Client;
using Jingyang.common;
using Jingyang.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Jingyang.login
{
    internal class LoginController : HttpClientBase
    {
        public LoginController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<string> LoginAsync(string userPhone, string userPassword)
        {
            return await Task.Factory.StartNew(() => Login(userPhone, userPassword));
        }

        public string Login(string userPhone, string userPassword)
        {
            try
            {
                var url = $"https://ctmingyi.com:18082/api/user/checkLogin?phone={userPhone}&password={userPassword}&imei=&isDoctor=false";
                var content = new JingyangBaseContent(url);
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
                    var userid = SaveUserInfo(userInfo);
                    return userid;
                }
                MainSession.SetStatus(MiaoProgress.Init);
                return null;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"登录异常{ex.Message}");
                return null;
            }
        }

        private string SaveUserInfo(JsonElement userInfo)
        {
            var user = userInfo.GetProperty("user");
            if (user.ValueKind == JsonValueKind.Null)
            {
                user = userInfo.GetProperty("vipUser");
            }
            var token = userInfo.GetProperty("token").NotNullString();
            var dicResult = JsonAnalysis.JsonToDic(user);
            dicResult.AddOrUpdate("token", token);
            var userId = dicResult[Constants.UserId].NotNullString();
            //MainSession.PlatformSession.AddOrUpdate(Constants.Token, token);
            //MainSession.PlatformSession.AddOrUpdate(userId, dicResult);
            //MainSession.PrintLogEvent.Publish(this, dicResult);

            return userId;
        }
    }
}
