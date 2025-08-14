using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using Utils;
using Utils.json;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Score
{
    internal class UnCollectedScoreController : HttpClientBase
    {
        public UnCollectedScoreController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<Dictionary<string, string>> GetUnCollectedScoreAsync(UserProject user)
        {
            return Task.Factory.StartNew(() => GetUnCollectedScore(user));
        }

        public Dictionary<string, string> GetUnCollectedScore(UserProject user)
        {
            try
            {
                var content = new UnCollectedScoreContent(user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]收集积分失败 - {response?.Message},请检查参数");
                    return null;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]收集积分失败: code={code}, message={msg}");
                    return null;
                }

                var result = root.GetProperty("result");
                return SummarizeScoreInfo(result, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]收集积分失败 - {ex.Message} - {ex.StackTrace}");
                return null;
            }
        }

        private Dictionary<string, string> SummarizeScoreInfo(JsonElement result, UserProject user)
        {
            var scoreDic = new Dictionary<string, string>();
            var scoreList = JsonAnalysis.JsonToDicList(result);
            if (!scoreList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]收集积分失败");
            }

            var total = 0;
            foreach(var scoreItem in scoreList)
            {
                var itemName = scoreItem.GetString("happenName");
                var itemId = scoreItem.GetString("id");
                var pointsSum = scoreItem.GetString("pointsSum");

                total += pointsSum.ToInt();
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]未收集积分【{itemName}】:{pointsSum}");
                scoreDic.AddOrUpdate(itemId, itemName);
            }

            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]未收集积分共【{total}】");

            return scoreDic;
        }
    }
}
