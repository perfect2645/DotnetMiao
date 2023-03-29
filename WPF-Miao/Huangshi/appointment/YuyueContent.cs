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

            var phoneEncode = JsReader.GetEncodeString(Order.ContactPhone);
            AddContent("lXDH", phoneEncode);

            var dateEncode = UnicodeConverter.EncodeOriginal(Order.Date, true);
            var name = Encryptor.ToUnicode(User.UserName);
            name = name.Replace("\\", "%25");

            AddContent("YYSJ", dateEncode);
            AddContent("YYSJD", Order.Time);
            AddContent("XM", name);
            AddContent("HYZT", Order.HYZT);
            AddContent("ZJLX", Order.ZJLX);
            AddContent("SFZHM", JsReader.GetEncodeString(Order.SFZHM));
            AddContent("DH", JsReader.GetEncodeString(Order.UserPhone));
            AddContent("XB", Order.XB);
            AddContent("CSRQ", Order.Birthday);
            AddContent("addItem", string.Empty);
            AddContent("addItemExt", string.Empty);
            AddContent("tjr", string.Empty);
            AddContent("zffs", Order.zffs);
            AddContent("tjkmm", Order.tjkmm);
            AddContent("zglb", Order.zglb);
            AddContent("xklb", Order.xklb);
            AddContent("mz", string.Empty);
            AddContent("cph", string.Empty);
            AddContent("bz", string.Empty);
            AddContent("yzm", string.Empty);
            AddContent("zyjxZK", 1);
            AddContent("saleName", string.Empty);
        }
    }
}
