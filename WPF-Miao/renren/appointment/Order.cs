using Base.Events;
using System;
using System.Text;

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

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"Date - {BookingDate}");
            sb.AppendLine($"Time - {StartTime} - {EndTime}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
