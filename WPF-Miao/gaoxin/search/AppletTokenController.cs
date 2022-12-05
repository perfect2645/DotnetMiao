using gaoxin.common;
using gaoxin.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace gaoxin.search
{
    internal class AppletTokenController : GaoxinControllerBase
    {
        public AppletTokenController(HttpClient httpClient) : base(httpClient)
        {
            ProcessAction = new Action<GaoxinContent>(GetToken);
        }

        private void GetToken(GaoxinContent content)
        {
            HttpDicResponse response = PostStringDecodeAsync(content, Decode).Result;
            var resultValue = CheckGetResultValue(response);
            if (resultValue.ValueKind == JsonValueKind.Null)
            {
                return;
            }

            SaveToken(resultValue);
        }

        private void SaveToken(JsonElement resultValue)
        {
            var wechatUserElement = resultValue.GetProperty("wechatUser");
            var wechatUser = JsonAnalysis.JsonToDic(wechatUserElement);
            MainSession.PlatformSession.AddOrUpdate("wechatUser", wechatUser);
        }
    }
}
