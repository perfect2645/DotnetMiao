using Longchi.common;
using Utils;

namespace Longchi.appointment
{
    internal class VerifyYuyueContent : LongchiContent
    {
        private static string url = "http://hpv_ym.zzytrj.net:15003/api/yuyue.php";
        public Order Order { get; private set; }
        public VerifyYuyueContent(Order order) : base(url, order.Cookie, order.FamilyId)
        {
            ContentType = "application/x-www-form-urlencoded";
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("cmd", "yuyue_return_id_get");
            AddContent("id", Order.ReturnId);
        }
    }
}
