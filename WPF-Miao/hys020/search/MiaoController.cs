using Base.session;
using HttpProcessor.Client;
using hys020.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

            var content = new DateCountContent(url);
            content.AddHeader("Cookie", JkSession.Cookie);
            content.BuildDefaultHeaders(Client);
            content.AddContent("yyDate", date);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    JkSession.PrintLogEvent.Publish(this, $"Search({date}) - response == null");
                    return;
                }

                var result = response.JsonBody.RootElement;
                var hasMiao = response.JsonBody.RootElement.GetProperty("doccustom");
                if (hasMiao.ValueKind == JsonValueKind.Null)
                {
                    return;
                }
                SearchInterval.StopInterval();
                AnalysisResult(result, date);
            }
            catch (Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"未查到苗{date} - {ex.Message} - {ex.StackTrace}");
            }
        }
    }
}
