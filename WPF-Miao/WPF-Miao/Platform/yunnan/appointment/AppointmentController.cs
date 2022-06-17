using HttpProcessor.Client;
using HttpProcessor.Container;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.getTimestamp;
using WPF_Miao.Platform.yunnan.model;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentController : HttpClientBase
    {

        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }



        public async Task AppointmentAsync()
        {
            var secureHeader = HttpServiceController.ServiceProvider.GetService<SecureHeader>();
            var content = HttpServiceController.ServiceProvider.GetService<AppointmentContent>();
            content.HttpRequestMessage.Method = HttpMethod.Post;

            await secureHeader.BuildHeader();
            content.AddHeaders(secureHeader.SecurityHeaderDic);

            var response = await Client.SendAsync(content.HttpRequestMessage);

            //var content = new AppointmentContent("h5-health.tengmed.com/api/gateway/VaccinationServer/immunizationAppointment");
            //var stringContent = content.GetJsonContent();
            //stringContent.Headers.Add("Content-Type", "application/json");
            //var response = await Client.PostAsync(requestMessage);


        }

        private void GetSignHeader()
        {
            //    var textToSign = "";
            //    var headerArr = ["X-Ca-Key", "X-Ca-Nonce", "X-Ca-Timestamp", "X-Content-MD5", "X-Service-Id", "X-Service-Method"];
            //    for (var i = 0; i < headerArr.length; i++)
            //    {
            //        var it = headerArr[i];
            //        var name = it.toLowerCase();
            //        var value = headers[it];
            //        textToSign += name + ":" + value + "&"
            //    }
            //    textToSign = textToSign.substring(0, textToSign.length - 1);
            //    var hash = CryptoJS.HmacSHA256(textToSign, $env.CASECRET);
            //    var signature = hash.toString(CryptoJS.enc.Base64);
            //    return signature

            var textToSign = "";
            var headerArr = new string[] { "X-Ca-Key", "X-Ca-Nonce", "X-Ca-Timestamp", "X-Content-MD5", "X-Service-Id", "X-Service-Method"};


        }
    }
}
