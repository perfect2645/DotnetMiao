using HttpProcessor.Content;
using suiyang.common;
using suiyang.session;
using System;
using Utils;

namespace suiyang.appointment
{
    internal class YuyueContent : HttpStringContent
    {
        private static string url = "http://www.jxy-tech.com/api/v1/locations/1/appoints";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url)
        {
            Order = order;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "www.jxy-tech.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("Authorization", MainSession.Auth);
            AddHeader("Accept", "*/*");
            //Cookie ？ noneed
            AddHeader("Referer", "http://www.jxy-tech.com/booking/companies/514966/?v=1");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Cookie", "JSESSIONID=EEC310DF733B864DB4F61D5AE511F96D");
            AddHeader("Origin", "http://www.jxy-tech.com");
        }

        private void BuildContent()
        {
            AddContent("type", "UNDEFINED");
            AddContent("btCode", Order.BtCode);
            AddContent("appointDate", Order.AppointDate);
            AddContent("beginTime", Order.BeginTime);
            AddContent("endTime", Order.EndTime);
            AddContent("openid", Order.OpenId);
            AddContent("barcode", Order.Barcode);
            AddContent("price", Order.Price);
            AddContent("priority", Order.Priority);
            AddContent("addressId", Order.AddressId);
            AddContent("identity", Order.Identity);
            AddContent("phone", Order.Phone);
            var nameU = Order.IdName.ToUnicode();
            AddContent("idName", nameU);
        }
    }
}
