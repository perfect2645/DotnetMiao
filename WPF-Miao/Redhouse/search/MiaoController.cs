using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using hys020.session;
using System;
using System.Collections.Generic;
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
            var miaoList = await Task.Factory.StartNew(() => SearchMiao());
            if (!miaoList.HasItem())
            {
                return;
            }

            MainSession.PrintLogEvent.Publish(this, $"查到苗");
            var attIdList = miaoList.Select(x => x[Constants.AttId].NotNullString()).ToList();
            MainSession.SetStatus(MiaoProgress.MiaoGet, attIdList);
        }

        private List<Dictionary<string, object>> SearchMiao()
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
                    Log($"SearchMiao Failed - {response.Message}");
                    return null;
                }

                var result = response.JsonBody.RootElement;
                var attList = response.JsonBody.RootElement.GetProperty("attList");
                if (attList.ValueKind == JsonValueKind.Null)
                {
                    Log($"未查到苗 - attList is null");
                    return null;
                }
                return AnalysisResult(attList);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return null;
            }
        }

        private List<Dictionary<string, object>> AnalysisResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDicList(jsonElement);

            var miaoList = dicResult.Where(x => StringUtil.NotEmpty(x[Constants.AttId].NotNullString(),
                x[Constants.OrgId].NotNullString())).ToList();

            if (!miaoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(null, $"MiaoController - 没查到苗");
                return null;
            }

            return miaoList;
        }
    }
}
