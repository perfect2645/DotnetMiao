using hys020.common;

namespace hys020.appointment
{
    internal class AppointContent : HysBaseContent
    {
        public Order Order { get; private set; }

        public AppointContent(Order order) : base(order.OrderUrl)
        {
            Order = order;
        }
    }
}
