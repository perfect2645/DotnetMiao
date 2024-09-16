using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Play
{
    internal class FragmentExchangeController : HttpClientBase
    {
        public FragmentExchangeController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void FragmentExchangeAsync(int activityGameId, int goodId)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() => FragmentExchange(user, activityGameId, goodId));
                }
            });

        }

        public void FragmentLotteryAsync(int activityGameId)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() => FragmentLottery(user, activityGameId));
                }
            });

        }

        public bool FragmentExchange(UserProject user, int activityGameId, int goodId)
        {
            try
            {
                var content = new FragmentExchangeContent(user, activityGameId, goodId);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange失败: code={code}, message={msg}");
                    return false;
                }

                var result = root.GetProperty("result");
                SaveBatResult(user, result);
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        public bool FragmentLottery(UserProject user, int activityGameId)
        {
            try
            {
                var content = new FragmentExchangeContent(user, activityGameId);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange失败: code={code}, message={msg}");
                    return false;
                }

                var result = root.GetProperty("result");
                SaveLotteryResult(user, result);
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private void SaveBatResult(UserProject user, JsonElement result)
        {
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - FragmentExchange");
        }
        private void SaveLotteryResult(UserProject user, JsonElement result)
        {
            var resultDic = JsonAnalysis.JsonToDic(result);

            var activityId = resultDic.GetString("activityId");
            var number = resultDic.GetString("number");
            var goodType = resultDic.GetString("goodType");
            var goodName = resultDic.GetString("goodName");
            var chanceNumber = resultDic.GetString("chanceNumber");
            var goodId = resultDic.GetString("goodId");
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - 抽中了 number[{number}-{goodId}-{goodName}],还有积分:{chanceNumber}");
        }
    }
}
