using Redhouse.common;
using System;

namespace Redhouse.appointment.Yuyue
{
    internal class YuyueContent : RedhouseContent
    {
        private const string url = "https://wxgzh.fckyy.org.cn/api/hosservice/Appointment/SaveReServerInfo";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url)
        {
            Order = order;
            InitContent();
        }

        private void InitContent()
        {
        }
    }
}
