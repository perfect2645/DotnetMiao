using Anze.common;
using Anze.login;
using Anze.session;
using Utils;

namespace Anze.appointment
{
    internal class YuyueContent : AnzeContent
    {
        private static string url = "https://ljzyyapi.yuanbaodaojia.com/v1/booking_vaccine";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            AddContent("ids", deptId);
            AddContent("uid", User.UserId);
        }
    }
}
