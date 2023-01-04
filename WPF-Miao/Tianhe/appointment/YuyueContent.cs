using Tianhe.common;
using Tianhe.login;
using Utils;

namespace Tianhe.appointment
{
    internal class YuyueContent : TianheContent
    {
        private static string url = "http://hpv_ym.zzytrj.net:15003/api/yuyue.php";
        public Order Order { get; private set; }
        public YuyueContent(Order order, TianheLogin user) : base(url, user)
        {
            ContentType = "application/x-www-form-urlencoded";
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("cmd", "yuyue_post");
        }
    }
}
