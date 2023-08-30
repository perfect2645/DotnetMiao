using Kuerle.common;
using Kuerle.login;
using Kuerle.session;
using System;
using System.Collections.Generic;
using Utils;

namespace Kuerle.appointment
{
    internal class YuyueContent : KuerleContent
    {
        private static string url = "https://bzjk.qiyingtian.com/hpv/ServerCommand/order";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("IDcard", Order.IDcard);
            AddContent("PID", Order.PID);
            AddContent("VID", Order.VID);
            AddContent("date", Order.Date.ToInt());
        }
    }
}
