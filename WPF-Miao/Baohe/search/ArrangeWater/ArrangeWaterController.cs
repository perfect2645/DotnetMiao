using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Baohe.search.ArrangeWater
{
    internal class ArrangeWaterController : HttpClientBase
    {
        public ArrangeWaterController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetArrangeWaterAsync(ISessionItem sessionItem, bool isPrintLog = false)
        {
            return Task.Factory.StartNew(() => GetArrangeWater(sessionItem, isPrintLog));
        }

        private void GetArrangeWater(ISessionItem sessionItem, bool isPrintLog = false)
        {
            var url = "https://appoint.yihu.com/appoint/do/doctorArrange/getArrangeWater";
            var content = new ArrangeWaterContent(url);
            //content.AddHeader("Cookie", content.BuildCookie());
            content.AddHeader("Cookie", sessionItem.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
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

            var arrangeWaters = AnalizeResult(result, sessionItem);

            if (isPrintLog)
            {
                sessionItem.PrintLogEvent.Publish(this, arrangeWaters, "ArrangeWater");
            }
        }

        private List<Dictionary<string, object>> AnalizeResult(JsonElement jsonElement, ISessionItem sessionItem)
        {
            var arrangeWater = JsonAnalysis.JsonToDicList(jsonElement);

            sessionItem.SessionDic.AddOrUpdate(Constant.ArrangeWater, arrangeWater);

            return arrangeWater;
        }
    }
}
