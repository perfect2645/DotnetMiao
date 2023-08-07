using Jksx.common;
using Jksx.login;

namespace Jksx.appointment
{
    internal class YuyueContent : JksxContent
    {
        private static string url = "https://uhapi.sxyygh.com/patient/order/ensure";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("hospitaluid", Order.Hospitaluid);
            AddContent("accesstype", Order.Accesstype);
            AddContent("resid", Order.Resid);
            AddContent("commonpeopleid", Order.Commonpeopleid);
            AddContent("workid", Order.Workid);
            AddContent("treattype", Order.Treattype);
            AddContent("mtype", Order.Mtype);
            AddContent("userid", Order.Userid);
        }
    }
}
