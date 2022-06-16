using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WPF_Miao.Platform.yunnan.getTimestamp;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentController : HttpClientBase
    {

        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }

        public GetTimestampController TimeContra { get; internal set; }

        public async Task AppointmentAsync()
        {
            var timestamp = await TimeContra.GetTimestamp();

            //var content = new AppointmentContent("h5-health.tengmed.com/api/gateway/VaccinationServer/immunizationAppointment");
            //var stringContent = content.GetJsonContent();
            //stringContent.Headers.Add("Content-Type", "application/json");
            //var response = await Client.PostAsync(requestMessage);


        }

        private string GetSignHeader()
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
            for (var i = 0; i < headerArr.Length; i++)
            {
                var it = headerArr[i];
                var name = it.ToLower();
                var value = headers[it];
                textToSign += name + ":" + value + "&";
            }

        }
    }
}
