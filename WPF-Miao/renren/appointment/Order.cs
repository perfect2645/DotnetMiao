using Base.Events;
using System;
using System.Text;
using Utils.timerUtil;

namespace renren.appointment
{
    public class Order
    {
        public string DateId { get; set; }
        public string BookingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ServiceStartTime { get; set; }
        public string ServiceEndTime { get; set; }
        public IntervalOnTime IntervalOnTime { get; private set; }
        public Action AppointAction { get; set; }
        public int Count { get; set; }

        public Order()
        {
            IntervalOnTime = new IntervalOnTime(AppointAction, StartTime, 800);
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"Date - {BookingDate}");
            sb.AppendLine($"Time - {StartTime} - {EndTime}");
            sb.AppendLine($"尝试次数 - {Count}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
