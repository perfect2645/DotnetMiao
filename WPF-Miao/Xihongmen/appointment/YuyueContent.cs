using Xihongmen.common;
using Xihongmen.session;
using System.Text;
using Utils;
using System;

namespace Xihongmen.appointment
{
    internal class YuyueContent : XhmContent
    {
        public Order Order { get; }

        private static readonly string url = "https://ctmingyi.com:18082/api/order/preOrder";

        public YuyueContent(Order schedule) : base(url)
        {
            Order = schedule;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Cookie", "PHPSESSID=ougmq0vvs8j08opsava1jela66");
        }

        private void BuildContent()
        {
            AddContent("type_id", Order.TypeId);
            AddContent("date", Order.Date);
            AddContent("type_title", Order.TypeTitle);
            AddContent("member_id", Order.UserId);
            AddContent("member_name", Order.UserName);
            AddContent("timeType", Order.TimeType);
            AddContent("key", Key);
            AddContent("token", MainSession.Token);
        }
    }
}
