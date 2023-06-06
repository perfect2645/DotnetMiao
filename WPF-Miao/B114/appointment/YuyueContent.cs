using B114.common;
using B114.login;
using System.Collections.Generic;

namespace B114.appointment
{
    internal class YuyueContent : B114Content
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/make/appointment/affirm";
        public Order Order { get; private set; }
        public YuyueContent(Order order, B114Login user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("hospitalCode", Order.HospitalCode);
            AddContent("list", BuildUserIdList());
            AddContent("makeAnAppointment", Order.MakeAnAppointment);
            AddContent("timeNo", Order.TimeNo);
            AddContent("vaccineInfoId", Order.VaccineInfoId);
        }

        private string[] BuildUserIdList()
        {
            var userIdList = new List<string>();
            userIdList.Add(User.UserId);

            return userIdList.ToArray();
        }

    }
}
