using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using hys020.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace hys020.search
{
    internal class MiaoController : HttpClientBase
    {
        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task SearchMiaoAsync()
        {
            await Task.Factory.StartNew(() => SearchMiao());
        }

        private void SearchMiao()
        {
            var deptId = MainSession.PlatformSession[Constants.DeptId];
            var url = $"http://www.hys020.com/home/doctorYyghMobileDate_{deptId}";

            var content = new MiaoContent(url);
            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao Failed - {response.Message}");
                    return;
                }

                var result = response.JsonBody.RootElement;
                var hasMiao = response.JsonBody.RootElement.GetProperty("doccustom");
                if (hasMiao.ValueKind == JsonValueKind.Null)
                {
                    return;
                }
                AnalysisResult(result);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);
            var code = dicResult["code"].ToInt();
            var message = dicResult["message"].NotNullString();

            var dataJsonElement = jsonElement.GetProperty("data");
            var data = JsonAnalysis.JsonToDic(dataJsonElement);

            if (code != 200 || !data.HasItem())
            {
                throw new HttpException($"code = {code}, message = {message}, data.count = {data?.Count}");
            }
            MainSession.PlatformSession.AddOrUpdate(data);
            MainSession.PrintLogEvent.Publish(this, data, $"保存App Sign");
        }
    }
}
