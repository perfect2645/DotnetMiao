using System.Net.Http;
using System.Text;
using Tjzs.common;
using Tjzs.session;
using Utils;

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
            Content.AddOrUpdate(string.Empty, Order.Content);
        }

        public override StringContent GetJsonContent()
        {
            return new StringContent(Order.Content, Encoding.UTF8, ContentType);
        }
    }
}
