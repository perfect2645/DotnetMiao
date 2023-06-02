using Dayim.login;
using System.Text;

namespace Dayim.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string MakeAnAppointment { get; set; }
        public string TimeNo { get; set; }
        public string VaccineInfoId { get; set; }
        public string OrderId { get; set; }
        public string Address { get; set; }
        public string HospitalCode { get; set; }
        public string ResultMsg { get; set; }

        internal DayimLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"Date - {MakeAnAppointment}");
            sb.AppendLine($"Time - {TimeNo}");
            sb.AppendLine($"HospitalCode - {HospitalCode}");
            sb.AppendLine($"VaccineInfoId - {VaccineInfoId}");
            sb.AppendLine($"OrderId - {OrderId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
