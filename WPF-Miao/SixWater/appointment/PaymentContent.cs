using SixWater.common;
using SixWater.login;
using SixWater.session;
using System;
using System.Collections.Generic;
using Utils;

namespace SixWater.appointment
{
    internal class PaymentContent : SixWaterContent
    {
        private static string url = "https://oss.zsqrmyy.com:8443/patient/register/payment";
        public Order Order { get; private set; }
        public PaymentContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("id", Order.OrderId);
            AddContent("payMethod", Order.PayMethod);
            AddContent("serviceProvider", Order.PayMethod);

        }
    }
}
