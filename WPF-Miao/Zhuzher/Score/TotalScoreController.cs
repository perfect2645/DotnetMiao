using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.json;
using Utils;
using Zhuzher.search;
using Zhuzher.session;
using Zhuzher.Common;
using System.Security.Policy;
using static System.Formats.Asn1.AsnWriter;

namespace Zhuzher.Score
{
    internal class TotalScoreController : HttpClientBase
    {

        private const string url = @"https://z.onewo.com/market/api/turntable/getScoreAccount";
        public TotalScoreController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetTotalScoreAsync(UserProject user)
        {
            Task.Factory.StartNew(() =>
            {
                GetTotalScore(user);
            });
        }

        public void GetTotalScore(UserProject user)
        {
            try
            {
                var content = new OnewoContent(url, user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]获取积分失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]获取积分失败: code={code}, message={msg}");
                    return;
                }

                var result = root.GetProperty("result");
                SummarizeScoreInfo(result, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]获取积分失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SummarizeScoreInfo(JsonElement result, UserProject user)
        {
            var scoreDic = JsonAnalysis.JsonToDic(result);
            if (!scoreDic.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]获取积分失败");
            }
            var total = scoreDic.GetString("score");
            var expiring = scoreDic.GetString("expiringScore");

            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]总积分【{total}】,快要过期积分【{expiring}】");
        }
    }
}
