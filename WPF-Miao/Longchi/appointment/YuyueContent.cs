using HttpProcessor.Content;
using Longchi.common;
using Longchi.session;
using System;
using Utils;

namespace Longchi.appointment
{
    internal class YuyueContent : LongchiContent
    {
        private static string url = "http://cnfw.mailiku.com/index/api/order.html";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.Cookie)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("user_id", Order.UserId);
            AddContent("time_id", Order.TimeId);
            AddContent("fid", Order.Fid);
            AddContent("yid", Order.Yid);
            AddContent("relation", Order.Relation);
            AddContent("__token__", Order.Token);
        }
    }
}
