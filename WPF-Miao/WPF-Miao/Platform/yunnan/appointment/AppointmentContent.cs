using HttpProcessor.Client;
using System;
using System.Net.Http;
using Utils;
using WPF_Miao.Platform.yunnan.session;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentContent : HttpClientContentBase
    {
        public string Cookie { get; set; }

        public AppointmentContent() : base()
        {
            BuildDefaultHeaders();
            BuildDefaultContents();
            BuildContentMd5Header();
        }

        #region Headers

        private void BuildDefaultHeaders()
        {
            HttpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            HttpRequestMessage.Headers.Add("X-Service-Id", "appoint.requestAppointRecordService");
            HttpRequestMessage.Headers.Add("Origin", "https://weixin.ngarihealth.com");
            HttpRequestMessage.Headers.Add("User-Agent", @"Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 Language/zh_CN");

            HttpRequestMessage.Headers.Add("X-Service-Method", "saveBuriedPointList");
            HttpRequestMessage.Headers.Add("Host", "weixin.ngarihealth.com");
            HttpRequestMessage.Headers.Add("Cookie", Cookie);// TODO
            HttpRequestMessage.Headers.Add("Referer", "https://weixin.ngarihealth.com/weixin/wx/mp/wxf119c4ff0a602d44/index.html?module=appointHomePage&source=wx_menu&code=031ZrqHa1VU5kD0p9kIa1vA1Qv0ZrqHU&state=STATE");
            HttpRequestMessage.Headers.Add("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            HttpRequestMessage.Headers.Add("encoding", "utf-8");
            HttpRequestMessage.Headers.Add("Accept", "*/*"); // TODO
        }

        internal void BuildContentMd5Header()
        {
            var md5HashArray = Encryptor.ToMD5Hash("[108346253]");
            var base64Md5 = Encryptor.EncryptBase64(md5HashArray);
            AddHeader(Constants.XContentMD5, base64Md5);
        }

        #endregion Headers

        #region Contents

        private void BuildDefaultContents()
        {
            var timeNow = DateTime.Now;
            var timeStart = timeNow.AddMinutes(-1);
            Contents.Add("time", timeNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Contents.Add("organId", "wxf119c4ff0a602d44");
            Contents.Add("appVersion", "2.9");
            Contents.Add("deviceType", "IOS");
            Contents.Add("serviceModule", "预约挂号申请页");
            Contents.Add("deviceNet", "wifi");
            Contents.Add("route", "weixin_appointapply_null_sw|weixin_appointapply_null_sw|weixin_appointapply_null_sw|weixin_appointapply_null_sw|weixin_appointapply_null_sw|");
            Contents.Add("eventId", "weixin_appointapply_checkapplyconditions_ck");
            Contents.Add("eventName", "申请页-点击提交按钮");
            Contents.Add("url", "eh.wx.health.doctor.AppointApply");
            Contents.Add("urlTitle", "预约挂号申请");
            Contents.Add("location", "");
            Contents.Add("userId", "");
            Contents.Add("startTime", timeStart.ToString("yyyy-MM-dd HH:mm:ss")); //TODO
        }

        #endregion Contents
    }
}
