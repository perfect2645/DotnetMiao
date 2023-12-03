using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Zhuzher.search;
using Zhuzher.session;


namespace Zhuzher.Play
{
    internal class PlayDetailController : HttpClientBase
    {
        public PlayDetailController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<MiaoshaItem>> GetPlayDetailsAsync(MiaoshaItem good)
        {
            return await Task.Factory.StartNew(() =>
            {
                return GetPlayDetails(good);
            });
        }

        public List<MiaoshaItem> GetPlayDetails(MiaoshaItem good)
        {
            var user = MainSession.UserProjectList.UserProjects.FirstOrDefault();
            try
            {
                var content = new PlayDetailContent(user, good);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GetPlayDetails - {response?.Message},请检查参数");
                    return null;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GetPlayDetails失败: code={code}, message={msg}");
                    return null;
                }

                var result = root.GetProperty("result");
                return CheckPlayDetails(result);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GetPlayDetails失败 - {ex.Message} - {ex.StackTrace}");
                return null;
            }
        }

        private List<MiaoshaItem> CheckPlayDetails(JsonElement result)
        {
            var playDetails = new List<MiaoshaItem>();

            return playDetails;
        }

        public void GoodAvailableAsync(MiaoshaItem good)
        {
            Task.Factory.StartNew(() =>
            {
                GoodAvailable(good);
            });
        }

        public bool GoodAvailable(MiaoshaItem good)
        {
            var user = MainSession.UserProjectList.UserProjects.FirstOrDefault();
            try
            {
                var content = new PlayDetailContent(user, good);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GetPlayDetails - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GetPlayDetails失败: code={code}, message={msg}");
                    return false;
                }

                var result = root.GetProperty("result");
                return CheckGoodResult(good, result);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GetPlayDetails失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckGoodResult(MiaoshaItem good, JsonElement result)
        {
            var games = JsonAnalysis.JsonToDicList(result.GetProperty("games"));
            if (!games.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"[GoodAvailable[{good.GoodName}]-失败: games isempty");
                return false;
            }
            var targetGame = games.FirstOrDefault(g => g.GetString("activityGameId") == good.ActivityGameId);
            if (targetGame == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"GoodAvailable-失败, No ActivityGameId [{good.ActivityGameId}] in games");
                return false;
            }

            //var targetGood = targetGame


            return true;
        }
    }
}
