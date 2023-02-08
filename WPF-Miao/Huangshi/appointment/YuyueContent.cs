using Huangshi.common;
using Huangshi.login;
using Huangshi.Encrypt;
using Utils;

namespace Huangshi.appointment
{
    internal class YuyueContent : MainContent
    {
        private static string url = "http://gzh.51kys.cn/hssfybjyjkglzx_web/order/ConfirmOrder";
        public Order Order { get; private set; }
        public YuyueContent(Order order, HuangshiLogin user) : base(url, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("ID", Order.UserId);

            //var phoneEncode = JsReader.GetEncodeString(User.Phone);
        }
    }
}
