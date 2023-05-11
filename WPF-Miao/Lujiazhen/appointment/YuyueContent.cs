using Lujiazhen.common;
using Lujiazhen.login;

namespace Lujiazhen.appointment
{
    internal class YuyueContent : LujiazhenContent
    {
        private static string url = "https://health-cn.xyz:9033/health_xcdp/api/hpv/save";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("name", Order.UserName);
            AddContent("phone", Order.Phone);
            AddContent("address", Order.Address);
            AddContent("type", Order.Type);
            AddContent("no", Order.No);
        }
    }
}
