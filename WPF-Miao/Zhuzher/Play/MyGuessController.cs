using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;
using static System.Net.WebRequestMethods;


namespace Zhuzher.Play
{
    internal class MyGuessController : HttpClientBase
    {
        private int _startGuessId;
        private int _endGuessId;

        public MyGuessController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetGuessResultAsync(int startGuessId, int endGuessId = 0)
        {
            _startGuessId = startGuessId;
            _endGuessId = endGuessId;
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() => GetGuessResult(user));
                }
            });

        }

        public bool GetGuessResult(UserProject user)
        {
            try
            {
                var url = $"https://z.onewo.com/market/api/activity/guess/page?size=20&current=1&activityId={MainSession.ActivityId}";
                var content = new OnewoContent(url, user);
                content.AddDeviceId();
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GuessBet - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GuessBet失败: code={code}, message={msg}");
                    return false;
                }

                var result = root.GetProperty("result");
                SaveGuessResult(user, result);
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GuessBet失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private void SaveGuessResult(UserProject user, JsonElement result)
        {
            var guessList = JsonAnalysis.JsonToDicList(result);

            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - GuessBet ********** Start");
            var allCorrect = true;
            var allFalse = true;
            foreach (var guessResult in guessList)
            {
                var activityGuessId = guessResult.GetString("activityGuessId").ToInt();
                if (activityGuessId == 0 || activityGuessId < _startGuessId)
                {
                    continue;
                }
                if (_endGuessId > 0 && activityGuessId > _endGuessId)
                {
                    continue;
                }
                var isCorrect = PrintOneGuessResult(user, guessResult);
                if (isCorrect)
                {
                    allFalse = false;
                }
                else 
                {
                    allCorrect = false;
                }
            }
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - 全部答对-{allCorrect}, 全部答错-{allFalse}");
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - GuessBet ********** End");
        }

        private bool PrintOneGuessResult(UserProject user, Dictionary<string, object> guessResult)
        {
            var activityGuessId = guessResult.GetString("activityGuessId").ToInt();
            var guessName = guessResult.GetString("guessName");
            var rightOptionId = guessResult.GetString("rightOptionId").ToInt();
            var myOptionId = guessResult.GetString("myOptionId").ToInt();

            var isCorrect = myOptionId == rightOptionId;
            var resultStr = isCorrect ? "答对了" : "答错了";
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - {activityGuessId}:{guessName}-{resultStr} ********** End");
            return isCorrect;
        }
    }
}