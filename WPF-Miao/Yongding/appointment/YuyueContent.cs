using Yongding.common;
using Yongding.login;

namespace Yongding.appointment
{
    internal class YuyueContent : YongdingContent
    {
        private static string url = "http://yiliao2.lefeiniu.com:8081/resource/add_yuyue";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("id", Order.Id);
            AddContent("timeid", Order.Timeid);
            AddEncodeContent("time", Order.Time);
            AddContent("no", Order.No);
            AddContent("cardid", Order.User.UserId);
            AddContent("token", Order.User.Token);
            AddContent("riqi", Order.Riqi);
            AddContent("gongzhong", string.Empty);
            AddContent("hy", string.Empty);
            AddContent("picture", string.Empty);
        }
    }
}
