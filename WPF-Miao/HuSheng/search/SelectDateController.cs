using HttpProcessor.Client;
using HttpProcessor.Content;
using HuSheng.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HuSheng.search
{
    internal class SelectDateController : HttpClientBase
    {
        public SelectDateController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task SearchByDateAsync(string date)
        {
            await Task.Factory.StartNew(() => SearchBydate(date));
        }

        public string SearchBydate(string date)
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_DateCount.do";

            var content = new SelectDateContent(url, date);
            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"Search({date}) - {response?.Message}");
                    return response?.Message;
                }

                var result = response.JsonBody.RootElement;
                var hasMiao = response.JsonBody.RootElement.GetProperty("doccustom");
                if (hasMiao.ValueKind == JsonValueKind.Null)
                {
                    var res = $"没查到苗 {date}";
                    Log(res);
                    return res;
                }
                //SearchInterval?.StopInterval();
                //AnalysisResult(result, date);
                return $"查到苗{date}";
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗{date} - {ex.Message} - {ex.StackTrace}");
                return $"{ex.Message}";
            }
        }
    }
}
