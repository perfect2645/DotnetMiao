using Jian.common;
using Jian.login;
using Jian.session;
using System;
using System.Collections.Generic;
using Utils;

namespace Jian.appointment
{
    internal class YuyueContent : JianContent
    {
        private static string baseUrl = "https://app.gocent.com.cn/unite/api/Reservation/SpecialReservasion?hospitalCode=";
        public Order Order { get; private set; }
        public YuyueContent(Order order, JianLogin user) : base(baseUrl, user)
        {
            RequestUrl = $"{BaseUrl}";
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("DoctorId", Order.DoctorId);
            AddContent("Dept2Code", Order.Dept2Code);
            AddContent("ExtCol", Order.ExtCol);
            AddContent("Amount", Order.Amount);
            AddContent("IdCardNo", Order.IdCardNo);
            AddContent("OutpatientNo", Order.OutpatientNo);
            AddContent("ReservationTime", Order.ReservationTime);
            AddContent("SeeADoctorTime", Order.SeeADoctorTime);
            AddContent("UserPhone", Order.UserPhone);
            AddContent("OrderType", 3);
        }
    }
}
