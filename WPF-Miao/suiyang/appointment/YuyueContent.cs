using suiyang.common;
using System;

namespace suiyang.appointment
{
    internal class YuyueContent : SuiyangBaseContent
    {
        private static string url = "http://www.jxy-tech.com/api/v1/locations/1/appoints";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("type", "UNDEFINED");
            AddContent("btCode", Order.Barcode);
            AddContent("appointDate", Order.AppointDate);
            AddContent("beginTime", Order.BeginTime);
            AddContent("endTime", Order.EndTime);
            AddContent("openid", Order.OpenId);
            AddContent("barcode", Order.Barcode);
            AddContent("price", Order.Price);
            AddContent("priority", Order.Priority);
            AddContent("addressId", Order.AddressId);
            AddContent("identity", Order.Identity);
            AddContent("identity", Order.Phone);
            AddContent("idName", Order.IdName);
        }
    }
}
