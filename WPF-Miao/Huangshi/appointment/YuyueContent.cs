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
            AddContent("ID", Order.DeptId);
            AddContent("TCSL", "1");
            AddContent("lXDH", JsReader.GetEncodeString(Order.ContactPhone));

            var dateEncode = UnicodeConverter.EncodeOriginal(Order.Date, true);
            var timeEncode = UnicodeConverter.EncodeOriginal(Order.Time, true);
            var name = UnicodeConverter.Encode(User.UserName);

            AddContent("YYSJ", dateEncode);
            AddContent("YYSJD", timeEncode);
            AddContent("XM", Order.DeptId);
            AddContent("HYZT", Order.DeptId);
            AddContent("ZJLX", Order.DeptId);
            AddContent("SFZHM", Order.DeptId);
            AddContent("DH", Order.DeptId);
            AddContent("XB", Order.DeptId);
            AddContent("CSRQ", Order.DeptId);
            AddContent("addItem", Order.DeptId);
            AddContent("addItemExt", Order.DeptId);
            AddContent("tjr", Order.DeptId);
            AddContent("zffs", Order.DeptId);
            AddContent("tjkmm", Order.DeptId);
            AddContent("zglb", Order.DeptId);
            AddContent("xklb", Order.DeptId);
            AddContent("mz", Order.DeptId);
            AddContent("cph", Order.DeptId);
            AddContent("bz", Order.DeptId);
            AddContent("yzm", Order.DeptId);
            AddContent("zyjxZK", Order.DeptId);
            AddContent("saleName", Order.DeptId);

            //var phoneEncode = JsReader.GetEncodeString(User.Phone);
        }
    }
}
