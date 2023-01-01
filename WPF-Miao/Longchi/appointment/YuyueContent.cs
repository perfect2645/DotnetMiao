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
        public YuyueContent(Order order) : base(url, order.Cookie, order.FamilyId)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("cmd", "yuyue_post");
            AddContent("qty", Order.Qty);
            AddContent("date", UnicodeConverter.Encode(Order.Date, true));
            AddContent("dizhi", UnicodeConverter.Encode(Order.Dizhi, true));
            AddContent("yuyue_name", UnicodeConverter.Encode(Order.YuyueName, true));
            AddContent("yuyue_user_code", Order.UserCode);
            AddContent("yuyue_user_add", UnicodeConverter.Encode(Order.YuyueUserAdd, true));
            AddContent("yuyue_user_suoshu", UnicodeConverter.Encode(Order.YuyueUserSuoshu, true));
        }
    }
}
