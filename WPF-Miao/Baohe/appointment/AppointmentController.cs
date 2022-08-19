using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Baohe.appointment
{
    internal class AppointmentController : HttpClientBase
    {
        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }


        public Task AppointmentAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => Appointment(sessionItem));
        }

        public void Appointment(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/registerInfo/register";
            var content = new AppointmentContent(url, sessionItem);
            content.AddHeader("Cookie", sessionItem.Cookie);
            content.AddHeader("Referer", content.BuildReferer());
            //content.AddHeader("Referer", sessionItem.Referer);

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Encode).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetDoctorList-{url} - {response.Body["Message"]}", "bad response");
            }

            var result = response.JsonBody.RootElement;
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:GetDoctorList-{url} - Result is empty", "empty result");
            }
            AnalizeResult(result, sessionItem);
        }

        private void AnalizeResult(JsonElement jsonElement, ISessionItem sessionItem)
        {
            var result = JsonAnalysis.JsonToDicList(jsonElement);

            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.DoctorList, result);

            sessionItem.PrintLogEvent.Publish(this, result, "预约成功");
        }
    }
}
