using Anze.common;
using Anze.login;
using Anze.session;
using Utils;

namespace Anze.appointment
{
    internal class YuyueContent : AnzeContent
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
            AddContent("uid", User.UserId);
        }
    }
}
