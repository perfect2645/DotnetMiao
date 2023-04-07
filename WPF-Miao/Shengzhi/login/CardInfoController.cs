using Base.viewmodel.status;
using HttpProcessor.Client;
using Shengzhi.common;
using Shengzhi.session;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.Encode;
using Utils.json;
using Utils.stringBuilder;

namespace Shengzhi.login
{
    internal class CardInfoController : HttpClientBase
    {
        public CardInfoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<string> LoginAsync(string userPhone, string userPassword)
        {
            return await Task.Factory.StartNew(() => Login(userPhone, userPassword));
        }

        private void AnalysisResult(string loginResult, ShengzhiLogin user)
        {
            try
            {
                var result = loginResult.AESDecrypt(Constants.EncodeKey, "", PaddingMode.PKCS7, CipherMode.ECB);

                var loginResultStr = result.ToTuple().Item2;
                var loginResultDic = loginResultStr.ToObjDic();
                if (!loginResultDic.ContainsKey("data"))
                {
                    MainSession.PrintLogEvent.Publish(this, "获取用户信息失败");
                    return;
                }

                var loginDataStr = loginResultDic["data"].NotNullString();
                var loginDataDic = loginDataStr.ToObjDic();

                user.LoginInfo = loginDataDic;
                MainSession.PrintLogEvent.Publish(this, loginResultDic, "获取用户信息成功");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"解析登录信息失败 -WechatLogin- {ex.Message}");
            }
        }

        public string Login(string userPhone, string userPassword)
        {
            try
            {
                var url = $"https://ctmingyi.com:18082/api/user/checkLogin?phone={userPhone}&password={userPassword}&imei=&isDoctor=false";
                var content = new WechatContent(url);
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
            MainSession.PlatformSession.AddOrUpdate(Constants.Token, token);
            MainSession.PlatformSession.AddOrUpdate(userId, dicResult);
            MainSession.PrintLogEvent.Publish(this, dicResult);

            return userId;
        }
    }
}
