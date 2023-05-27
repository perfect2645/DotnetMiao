using Lzy.common;
using Lzy.login;
using System;
using System.Collections.Generic;

namespace Lzy.appointment
{
    internal class YuyueContent : LzyContent
    {
        private static string baseUrl = "http://wechat.yunhebj.com/app/index.php?i=30&c=entry&do=course&m=lzl_course&";
        public Order Order { get; private set; }
        public YuyueContent(Order order, LzyLogin user) : base(baseUrl, user)
        {
            Order = order;
            RequestUrl = $"{baseUrl}id={order.DeptId}&date={order.Date}&week={order.Week}";
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("op", "post");
            AddContent("time_id", Order.TimeId);
            AddEncodeContent("user_name", Order.UserName);
            AddEncodeContent("mobile", Order.Mobile);
        }
    }
}
