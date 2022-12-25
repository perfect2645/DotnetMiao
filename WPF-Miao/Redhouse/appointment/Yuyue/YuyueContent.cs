using Redhouse.common;

namespace Redhouse.appointment.Yuyue
{
    internal class YuyueContent : RedhouseContent
    {
        private const string url = "";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url)
        {
            Order = order;
        }
    }
}
