using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Baohe.appointment
{
    internal class AppointmentContent : HttpStringContent
    {
        private readonly ISessionItem Session;

        public AppointmentContent(string url, ISessionItem session) : base(url)
        {
            Session = session;
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            AddHeader("Host", "appoint.yihu.com");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "https://appoint.yihu.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36");

            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            base.BuildDefaultHeaders(httpClient);
        }

        private void BuildContent()
        {
            var doctorOrder = BuildDoctorOrder();
            AddContent("doctorRegOrder", doctorOrder);
        }

        private Dictionary<string, object> BuildDoctorOrder()
        {
            var sessionDic = Session.SessionDic;
            var doctorRegOrder = new Dictionary<string, object>();
            doctorRegOrder.Add(Constant.WaterId, sessionDic[Constant.WaterId]);
            doctorRegOrder.Add(Constant.WaitingInfor, sessionDic[Constant.WaitingInfor]);
            doctorRegOrder.Add("store", "");
            doctorRegOrder.Add("serialNo", "1");
            doctorRegOrder.Add("memberSn", "54985240");
            doctorRegOrder.Add("memberName", "刘振宇");
            doctorRegOrder.Add("memberPhone", "13940897525");
            doctorRegOrder.Add("memberAddress", "东城白云山");
            doctorRegOrder.Add("memberBirthday", "2000-03-10");
            doctorRegOrder.Add("memberSex", "2");
            doctorRegOrder.Add("memberId", "54985240");
            doctorRegOrder.Add("othercard", "");
            doctorRegOrder.Add("guardianmembersn", "");
            doctorRegOrder.Add("identitytype", "1");
            doctorRegOrder.Add("memberZhengjianid", "110***********5689");
            doctorRegOrder.Add("memberIdcard", "110***********5689");
            doctorRegOrder.Add("accManageGoodSn", null);
            doctorRegOrder.Add("regpaytype", "");
            doctorRegOrder.Add("cliniccard", "");
            doctorRegOrder.Add("applyNo", "");
            doctorRegOrder.Add("mobile", "13940897525");
            doctorRegOrder.Add("doctorSn", "710869460");
            doctorRegOrder.Add("hosDeptId", "7175975");
            doctorRegOrder.Add("hosDeptName", "儿童保健科");
            doctorRegOrder.Add("hospitalId", "1040231");
            doctorRegOrder.Add("hospitalName", "合肥市蜀山区南岗镇卫生院");
            doctorRegOrder.Add("doctorService_gh", "2");
            doctorRegOrder.Add("doctorUid", "710659564");
            doctorRegOrder.Add("doctorName", "儿保科医生");
            doctorRegOrder.Add("doctorPic", "");
            doctorRegOrder.Add("doctorClinicName", "主治医师");
            doctorRegOrder.Add("GH_HosProId", "12");
            doctorRegOrder.Add("GH_HosProName", "安徽");
            doctorRegOrder.Add("GH_HosCityId", "101");
            doctorRegOrder.Add("GH_HosCityName", "合肥");
            doctorRegOrder.Add("registerDate", "2022-08-12");
            doctorRegOrder.Add("timeId", 1);
            doctorRegOrder.Add("arrangeId", 160023903);
            doctorRegOrder.Add("ghAmount", 0);
            doctorRegOrder.Add("securityDeposit", 0);
            doctorRegOrder.Add("ghfeeway", 0);
            doctorRegOrder.Add("ModeId", 0);
            doctorRegOrder.Add("GhFee", 0);
            doctorRegOrder.Add("AllFee", 0);
            doctorRegOrder.Add("availablenum", 9);
            doctorRegOrder.Add("UnOpened", false);
            doctorRegOrder.Add("FHTimes", "23:00");
            doctorRegOrder.Add("FHDays", "2");
            doctorRegOrder.Add("accountSn", 148528129);
            doctorRegOrder.Add("cardNumber", "2078631533");
            doctorRegOrder.Add("loginId", "od0AjwJ0eodDYYA4pUfDRUCIIm8E");
            doctorRegOrder.Add("channelId", "9000370");
            doctorRegOrder.Add("utm_source", "0.0.h.1026.bus010.0__0.0.h.1026.bus010.0");
            doctorRegOrder.Add("doctorOfficeName", "");
            doctorRegOrder.Add("retId", "8f1adb4a37e143a885d53db93f803eeb");

            return doctorRegOrder;
        }
    }
}
