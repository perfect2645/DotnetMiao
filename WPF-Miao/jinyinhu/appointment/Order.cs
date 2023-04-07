using Base.Events;
using System;
using System.Text;
using Utils.timerUtil;

namespace jinyinhu.appointment
{
    public class Order
    {
        public IntervalOnTime IntervalOnTime { get; private set; }
        public string TimeId { get; set; }
        public string ReservationDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string YeWuId { get; set; }
        public string ReservationTime { get; set; }
        public string AppointmentType { get; set; }


        public Order()
        {
            IntervalOnTime = new IntervalOnTime(TimeId, 600);
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"YeWuId - {YeWuId}");
            sb.AppendLine($"AppointmentType - {AppointmentType}");
            sb.AppendLine($"ReservationDate - {ReservationDate}");
            sb.AppendLine($"ReservationTime - {ReservationTime}");
            sb.AppendLine($"TimeId - {TimeId}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
