using HttpProcessor.Content;
using Longchi.common;
using Longchi.session;
using System;
using Utils;

namespace Longchi.appointment
{
    internal class YuyueContent : LongchiContent
    {
        private static string url = "http://hpv_ym.zzytrj.net:15003/api/yuyue.php";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.Cookie)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("cmd", "yuyue_post");
            AddContent("qty", Order.Qty);
            AddContent("dizhi", Order.Dizhi);
            AddContent("yuyue_name", Order.YuyueName);
            AddContent("yuyue_user_code", Order.UserCode);
            AddContent("yuyue_user_add", Order.YuyueUserAdd);
            AddContent("yuyue_user_suoshu", Order.YuyueUserSuoshu);
        }
    }
}
