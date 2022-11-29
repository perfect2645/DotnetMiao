using Base.Events;
using System;
using System.Text;
using Utils.timerUtil;

namespace jieyang.appointment
{
    public class Order
    {
        public IntervalOnTime IntervalOnTime { get; private set; }
        public string Regtype { get; set; }
        public string Type { get; set; } = "reg";
        public string DeptId { get; set; }
        public string DeptName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string OrgName { get; set; }
        public string CustomerName { get; set; } = "赵梦云";
        public string CustomerId { get; set; } = "78083792";
        public string AppointAmount { get; set; }
        public string AppointDate { get; set; }
        public string TimeRange { get; set; }
        public string ScheduleId { get; set; }
        public string Bco01 { get; set; }

        public Order()
        {
            IntervalOnTime = new IntervalOnTime(ScheduleId, 600);
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"CustomerId - {CustomerId}");
            sb.AppendLine($"CustomerName - {CustomerName}");
            sb.AppendLine($"DoctorName - {DoctorName}");
            sb.AppendLine($"AppointDate - {AppointDate}");
            sb.AppendLine($"TimeRange - {TimeRange}");
            sb.AppendLine($"ScheduleId - {ScheduleId}");
            sb.AppendLine($"Bco01 - {Bco01}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
