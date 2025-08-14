using Jingjiang.common;

namespace Jingjiang.appointment
{
    internal class YuyueContent : JingjiangContent
    {
        private static string url = "http://yygh.well-bone.com/prod-api/system/SysYwBook";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("ywId", Order.YwId);
            AddContent("ywDateId", Order.YwDateId);
            AddContent("datetimeId", Order.DatetimeId);
            AddContent("name", Order.UserName);
            AddContent("sfz", Order.Sfz);
            AddContent("sjhm", Order.Sjhm);
            AddContent("dwCode", Order.DwCode);
            AddContent("zxx", "");
        }
    }
}
