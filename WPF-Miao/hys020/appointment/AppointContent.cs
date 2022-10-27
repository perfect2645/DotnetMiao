using HttpProcessor.Content;

namespace hys020.appointment
{
    internal class AppointContent : HttpStringContent
    {
        public Order Order { get; private set; }

        public AppointContent(string url, Order order) : base(url)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
