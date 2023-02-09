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
            var name = Encryptor.ToUnicode(User.UserName);

            AddContent("YYSJ", dateEncode);
            AddContent("YYSJD", timeEncode);
            AddContent("XM", name);
            AddContent("HYZT", Order.HYZT);
            AddContent("ZJLX", Order.ZJLX);
            AddContent("SFZHM", JsReader.GetEncodeString(Order.SFZHM));
            AddContent("DH", JsReader.GetEncodeString(Order.DeptId));
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
