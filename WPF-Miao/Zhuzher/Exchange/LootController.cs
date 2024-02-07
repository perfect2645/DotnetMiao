using HttpProcessor.Client;
using HttpProcessor.JsonFactory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class LootController : HttpClientBase
    {
        public LootController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void LootAsync()
        {
            var lootItemList = new List<LootItem>()
            {
                new LootItem
                {
                    ActivityGameId = 1493,
                    ActivityId = MainSession.ActivityId.ToInt(),
                    GameGoodId = 8425,
                    Number = 10
                }
            };
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    foreach (var lootItem in lootItemList)
                    {
                        Thread.Sleep(500);
                        Task.Factory.StartNew(() => Loot(user, lootItem));
                    }
                }
            });

        }

        public bool Loot(UserProject user, LootItem lootItem)
        {
            try
            {
                var content = new LootContent(user, lootItem);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]Loot - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]Loot失败: code={code}, message={msg}");
                    return false;
                }

                var result = root.GetProperty("result");
                SaveLootResult(user, result);
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]Loot失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private void SaveLootResult(UserProject user, JsonElement result)
        {
            var lootCodeList = result.GetRawText();
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]Loot成功 - lootCodeList:{lootCodeList}");
        }
    }
}
