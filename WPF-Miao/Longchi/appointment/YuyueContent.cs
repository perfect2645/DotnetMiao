using Longchi.common;
using Utils;

namespace Longchi.appointment
{
    internal class YuyueContent : LongchiContent
    {
        private static string url = "http://hpv_ym.zzytrj.net:15003/api/yuyue.php";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.Cookie, order.FamilyId)
        {
            ContentType = "application/x-www-form-urlencoded";
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("cmd", "yuyue_post");
            AddContent("qty", Order.Qty);

            var dateEncode = UnicodeConverter.EncodeOriginal(Order.Date, true);
            AddContent("date", dateEncode);
            AddContent("dizhi", UnicodeConverter.Encode(Order.Dizhi, true));

            var encodeName = UnicodeConverter.Encode(Order.YuyueName, true);
            AddContent("yuyue_name", encodeName);
            AddContent("yuyue_user_code", Order.UserCode);
            AddContent("yuyue_user_add", UnicodeConverter.Encode(Order.YuyueUserAdd, true));
            AddContent("yuyue_user_suoshu", UnicodeConverter.Encode(Order.YuyueUserSuoshu, true));
        }
    }
}
