using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using hys020.session;
using System;
using System.Linq;
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
                var attList = response.JsonBody.RootElement.GetProperty("attList");
                if (attList.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"未查到苗 - attList is null");
                    return;
                }
                AnalysisResult(attList);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDicList(jsonElement);

            var miaoList = dicResult.Where(x => StringUtil.NotEmpty(x[Constants.AttId].NotNullString(),
                x[Constants.OrgId].NotNullString())).ToList();

            if (!miaoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没查到苗");
                return;
            }

            MainSession.MiaoSession.AddOrUpdate("MiaoList", miaoList);
            MainSession.PrintLogEvent.Publish(this, miaoList, $"查到苗");
            MainSession.SetStatus(MiaoProgress.MiaoGet);
            //var dataJsonElement = jsonElement.GetProperty("data");
            //   var data = JsonAnalysis.JsonToDic(dataJsonElement);

            //   if (code != 200 || !data.HasItem())
            //   {
            //       throw new HttpException($"code = {code}, message = {message}, data.count = {data?.Count}");
            //   }
            //   MainSession.PlatformSession.AddOrUpdate(data);
            //   MainSession.PrintLogEvent.Publish(this, data, $"保存App Sign");   
        }
    }
}
