using HttpProcessor.Client;
using Huaxi.login;
using Huaxi.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Huaxi.Yzm
{
    internal class YzmController : HttpClientBase
    {
        public YzmController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Yzm> GetYzmAsync(HuaxiLogin user)
        {
            return await Task.Factory.StartNew(() => GetYzm(user));
        }

        private Yzm GetYzm(HuaxiLogin user)
        {
            try
            {
                var content = new YzmContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return null;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetString();
                var msg = root.GetProperty("msg").GetString();
                if (code != "1")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取验证码失败: code={code}, msg={msg}");
                    return null;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取验证码失败: results is empty");
                    return null;
                }
                return SaveYzm(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取验证码异常{ex.Message}");
                return null;
            }
        }

        private Yzm SaveYzm(JsonElement data, HuaxiLogin user)
        {
            var yzmData = JsonAnalysis.JsonToDic(data);
            var yzm = new Yzm();
            yzm.BizSeq = yzmData.GetString("bizSeq");
            yzm.ImageData = yzmData.GetString("imageData");
            yzm.UserName = user.UserName;

            MainSession.PrintLogEvent.Publish(this, $"{yzm.UserName}获取验证码成功");

            return yzm;
        }
    }
}
