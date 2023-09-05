using Baohe.constants;
using Baohe.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.json;

namespace Baohe.search.ArrangeWater
{
    internal class ArrangeWaterController : HttpClientBase
    {
        public ArrangeWaterController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<bool> GetArrangeWaterAsync()
        {
            return Task.Factory.StartNew(() => GetArrangeWater());
        }

        #region Appoint Water

        private bool GetArrangeWater()
        {
            MainSession.PrintLogEvent.Publish(this, $"GetArrangeWater Start");
            var url = "https://appoint.yihu.com/appoint/do/doctorArrange/getArrangeWater";
            var content = new ArrangeWaterContent(url);
            content.AddHeader("Cookie", MainSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            MainSession.PrintLogEvent.Publish(this, $"GetArrangeWater End");
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetArrangeWater-{url} - {response.Body["Message"]}", "Arrange Water");
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:GetArrangeWater-{url} - Result is empty", "empty result");
            }

            var arrangeWaters = AnalysisResult(result);

            //if (isPrintLog)
            //{
            //    MainSession.PrintLogEvent.Publish(this, arrangeWaters, "ArrangeWater");
            //}

            return arrangeWaters.HasItem();
        }

        private List<Dictionary<string, object>> AnalysisResult(JsonElement jsonElement)
        {
            var arrangeWater = JsonAnalysis.JsonToDicList(jsonElement);
            var arrangeWaterList = SessionBuilder.GetAvailableArrangeWater(arrangeWater);

            if (!arrangeWaterList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"{Constant.ProjectName}:查苗成功-没有可用苗");
                return null;
            }

            MainSession.AddMiaoSession(Constant.ArrangeWater, arrangeWaterList);

            return arrangeWaterList;
        }

        #endregion Apporint Water

        #region Exchange Water

        public Task<bool> GetExchangeWaterAsync(string arrangeId)
        {
            return Task.Factory.StartNew(() => GetExchangeWater(arrangeId));
        }

        private bool GetExchangeWater(string arrangeId)
        {
            MainSession.PrintLogEvent.Publish(this, $"GetArrangeWater Start");
            var url = "https://appoint.yihu.com/appoint/do/doctorArrange/getArrangeWater";
            var content = new ArrangeWaterContent(url);
            content.AddHeader("Cookie", MainSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            MainSession.PrintLogEvent.Publish(this, $"GetArrangeWater End");
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetArrangeWater-{url} - {response.Body["Message"]}", "Arrange Water");
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:GetArrangeWater-{url} - Result is empty", "empty result");
            }

            var arrangeWaters = BuildExchangeResult(result, arrangeId);

            //if (isPrintLog)
            //{
            //    MainSession.PrintLogEvent.Publish(this, arrangeWaters, "ArrangeWater");
            //}

            return arrangeWaters.HasItem();
        }

        private List<Dictionary<string, object>> BuildExchangeResult(JsonElement jsonElement, string arrangeId)
        {
            var arrangeWater = JsonAnalysis.JsonToDicList(jsonElement);
            var exchangeWater = SessionBuilder.GetTargetWater(arrangeWater, arrangeId);

            if (!exchangeWater.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"{Constant.ProjectName}:查苗成功-没有可用苗");
                return null;
            }

            var waterList = new List<Dictionary<string, object>>();
            waterList.Add(exchangeWater);

            MainSession.AddMiaoSession(Constant.ArrangeWater, waterList);

            return waterList;
        }

        #endregion Exchange Water
    }
}
