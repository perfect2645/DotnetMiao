using Base.Events;
using System;
using System.Text;
using Utils.timerUtil;

namespace chutian.appointment
{
    public class Order
    {
        public IntervalOnTime IntervalOnTime { get; private set; }
        public string UserId { get; set; }
        public string DoctorId { get; set; }
        public string FamilyId { get; set; }
        public string ScheduleId { get; set; }
        public string ParttimeId { get; set; } = string.Empty;
        public string Hospitalid { get; set; }
        public string MiaoId { get; set; }

        public string OrderUrl 
        { 
            get { return BuildUrl(); }
        }

        public Order()
        {
            IntervalOnTime = new IntervalOnTime(ScheduleId, 2000);
        }

        private string BuildUrl()
        {
            var urlHead = "https://ctmingyi.com:18082/api/order/getOrderById";
            var url = $"{urlHead}/{MiaoId}/{Hospitalid}";
            return url;
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约成功 *********");

            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"ScheduleId - {ScheduleId}");
            sb.AppendLine($"MiaoId - {MiaoId}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
