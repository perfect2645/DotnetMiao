using Tjzs.common;

namespace Tjzs.appointment
{
    internal class YuyueContent : TjzsContent
    {
        private static string url = "https://tjzsprod.tsxcx.xyz/tjzs/v1/formValue/publish";

        public YuyueContent(Order order) : base(url, order)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("name", Order.UserName);
            AddContent("phone", Order.Phone);
            AddContent("address", Order.Address);
            AddContent("type", Order.Type);
            AddContent("no", Order.No);
        }
    }
}
