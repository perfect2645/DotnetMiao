using Base.Events;
using System;
using System.Text;

namespace chutian.appointment
{
    public class Order
    {
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

        private string BuildUrl()
        {
            var urlHead = "https://ctmingyi.com:18082/api/order/getOrderById";
            var url = $"{urlHead}/{MiaoId}/{Hospitalid}";
            return url;
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"ScheduleId - {ScheduleId}");
            sb.AppendLine($"MiaoId - {MiaoId}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
