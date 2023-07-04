using Dalian.common;
using Dalian.login;
using Dalian.session;
using System;
using System.Reflection.Emit;
using Utils;

namespace Dalian.appointment
{
    internal class YuyueContent : DalianContent
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/order/save?_time=";
        public Order Order { get; private set; }
        public YuyueContent(Order order, DalianLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildHeader();
            BuildContent();
        }

        protected override void BuildHeader()
        {
            base.BuildHeader();
            BuildReferer();
        }

        private void BuildReferer()
        {
            var hosCode = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var firstDept = MainSession.PlatformSession.GetString(Constants.FirstDeptCode);
            var secondDept = MainSession.PlatformSession.GetString(Constants.DeptId);
            AddHeader("Referer", $"https://www.114yygh.com/wechat/hospital/submission?hosCode={hosCode}&firstDeptCode={firstDept}&secondDeptCode={secondDept}&target={Order.TreatmentDay}&uniqProductKey={Order.UniqProductKey}&dutyTime={Order.DutyTime}");
        }

        private void BuildContent()
        {
            AddContent("hosCode", Order.HosCode);
            AddContent("firstDeptCode", Order.FirstDeptCode);
            AddContent("secondDeptCode", Order.SecondDeptCode);
            AddContent("treatmentDay", Order.TreatmentDay);
            AddContent("uniqProductKey", Order.UniqProductKey);
            AddContent("cardType", Order.CardType);
            AddContent("cardNo", Order.CardNo);
            AddContent("smsCode", Order.SmsCode);
            AddContent("jytCardId", Order.JytCardId);
            AddContent("hospitalCardId", Order.HospitalCardId);
            AddContent("phone", Order.Phone);
            AddContent("dutyTime", Order.DutyTime);
            AddContent("orderFrom", Order.OrderFrom);
        }
    }
}
