using Huangdai.common;

namespace Huangdai.appointment
{
    internal class YuyueContent : HuangdaiContent
    {
        private static string url = "https://health-cn.xyz:9033/health_xcdp/api/hpv/save";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url)
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
