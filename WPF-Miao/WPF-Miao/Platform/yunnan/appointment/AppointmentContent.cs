using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentContent : HttpClientContentBase
    {
        public string Cookie { get; set; }

        public AppointmentContent() : base()
        {
            BuildDefaultHeaders();
            BuildDefaultContents();
        }

        #region Headers

        private void BuildDefaultHeaders()
        {
            HttpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            HttpRequestMessage.Headers.Add("X-Service-Id", "appoint.requestAppointRecordService");
            HttpRequestMessage.Headers.Add("Origin", "https://weixin.ngarihealth.com");
            HttpRequestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/8.0.23(0x18001727) NetType/WIFI Language/zh_CN");

            HttpRequestMessage.Headers.Add("X-Service-Method", "saveBuriedPointList");
            HttpRequestMessage.Headers.Add("Host", "weixin.ngarihealth.com");
            HttpRequestMessage.Headers.Add("Cookie", Cookie);// TODO
            HttpRequestMessage.Headers.Add("Referer", "https://weixin.ngarihealth.com/weixin/wx/mp/wxf119c4ff0a602d44/index.html?module=appointHomePage&source=wx_menu&code=031ZrqHa1VU5kD0p9kIa1vA1Qv0ZrqHU&state=STATE");
            HttpRequestMessage.Headers.Add("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            HttpRequestMessage.Headers.Add("encoding", "utf-8");
            HttpRequestMessage.Headers.Add("Accept", "*/*"); // TODO
        }



        #endregion Headers

        #region Contents

        private void BuildDefaultContents()
        {

        }

        #endregion Contents
    }
}
