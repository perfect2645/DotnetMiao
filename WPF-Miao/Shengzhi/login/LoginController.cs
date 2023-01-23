using Base.viewmodel.status;
using Shengzhi.common;
using Shengzhi.session;
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
using Utils.Encode;
using Base.session;
using System.Security.Cryptography;

namespace Shengzhi.login
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

        public void WechatLoginAsync(ShengzhiLogin user)
        {
            Task.Factory.StartNew(() => WechatLogin(user));
        }

        public void WechatLogin(ShengzhiLogin user)
        {
            try
            {
                var url = $"{user.Url}";
                var content = new WechatContent(url, user);

                var qy = content.BuildGetQyCheckSuffix();

                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"登录失败 - {response?.Message},请检查参数");
                    return;
                }
                var loginResult = response.JsonBody.RootElement.GetProperty("result").NotNullString();
                if (string.IsNullOrEmpty(loginResult))
                {
                    MainSession.PrintLogEvent.Publish(this, $"登录失败 - result为空,请检查参数");
                    return;
                }

                AnalysisResult(loginResult, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"登录异常{ex.Message}");
            }
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
            catch(Exception ex)
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
            var userId = dicResult[Constants.UserID].NotNullString();
            MainSession.PlatformSession.AddOrUpdate(Constants.Token, token);
            MainSession.PlatformSession.AddOrUpdate(userId, dicResult);
            MainSession.PrintLogEvent.Publish(this, dicResult);

            return userId;
        }
    }
}
