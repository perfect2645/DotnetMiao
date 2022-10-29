using hys020.common;

namespace hys020.appointment
{
    internal class AppointContent : HysBaseContent
    {
        public Order Order { get; private set; }

        public AppointContent(string url, Order order) : base(url)
        {
            Order = order;
        }
    }
}
