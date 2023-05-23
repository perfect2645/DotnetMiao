using Kuerle.common;
using Kuerle.login;
using Kuerle.session;
using Utils;

namespace Kuerle.appointment
{
    internal class YuyueContent : KuerleContent
    {
        private static string url = "https://yuyue.azjkzx.cn/api/index/addorder";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("ids", Order.Ids.ToInt());
        }
    }
}
