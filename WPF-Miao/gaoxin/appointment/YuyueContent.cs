using gaoxin.common;
using gaoxin.session;
using Utils;

namespace gaoxin.appointment
{
    internal class YuyueContent : GaoxinContent
    {
        private static string url = "https://ymglfw.care4u.cn/npApii/slVaccineDispark/addUserVaccine";
        public Order Order { get; }
        public YuyueContent(Order order) : base(url, "预约", null)
        {
            Order = order;
            BuildHeaderPlus();
            BuildContent();
        }

        private void BuildHeaderPlus()
        {
            AddHeader("token", Order.OrderToken);
        }

        private void BuildContent()
        {
            AddContent("daid", Order.daid);
            AddContent("yysj", Order.yysj);
            AddContent("yysjd", Order.yysjd);
            AddContent("yyymid", Order.yyymid);
            AddContent("disparkId", Order.disparkId);
            AddContent("yyjg", Order.yyjg);
            AddContent("jgid", Order.jgid);
            AddContent("jgcommid", Order.jgcommid);
        }
    }
}
