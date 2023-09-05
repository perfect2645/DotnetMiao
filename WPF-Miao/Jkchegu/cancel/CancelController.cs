using HttpProcessor.Client;
using HttpProcessor.Content;
using Jkchegu.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jkchegu.cancel
{
    internal class CancelController : HttpClientBase
    {
        public CancelController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task CancelAllAsync(string date)
        {
            await Task.Factory.StartNew(() => CancelAll(date));
        }

        public string CancelAll(string date)
        {
            var url = "http://app.whkfqws.com/wx-mobile/Vaccination/cancelReservation.do";

            var etid = JkSession.DefaultEtid;
            var content = new CancelContent(etid, url);
            var defaultUser = JkSession.DefaultUser;
            content.AddHeader("Cookie", defaultUser.Session);
            content.BuildDefaultHeaders(Client);
            content.AddContent("yyDate", date);

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
                    var res = $"取消失败 {date}";
                    Log(res);
                    return res;
                }

                return "取消成功";
            }
            catch (Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"取消失败{date} - {ex.Message} - {ex.StackTrace}");
                return $"{ex.Message}";
            }
        }
    }
}

