using Haikou.common;
using Haikou.login;
using Haikou.session;
using System;
using Utils;

namespace Haikou.appointment
{
    internal class YuyueContent : HaikouContent
    {
        private static string url = "https://wx.hospite.com/hkfy/schedule/appointment";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
