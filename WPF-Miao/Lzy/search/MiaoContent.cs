using Lzy.appointment;
using Lzy.common;
using Lzy.login;
using Lzy.session;
using Utils;

namespace Lzy.search
{
    internal class MiaoContent : LzyContent
    {
        private static string baseUrl = "http://wechat.yunhebj.com/app/index.php?i=30&c=entry&do=course&m=lzl_course&";

        public MiaoContent(LzyLogin user, Order order) : base(baseUrl, user)
        {
            RequestUrl = $"{baseUrl}id={order.DeptId}&date={order.Date}&week={order.Week}";
            BuildContent();
        }

        private void BuildContent()
        {
        }
    }
}
