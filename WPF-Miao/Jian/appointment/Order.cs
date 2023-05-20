using Jian.login;
using System.Text;

namespace Jian.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string DoctorId { get; set; }
        public string Dept2Code { get; set; }
        public string ExtCol { get; set; }
        public string Amount { get; set; }
        public string IdCardNo { get; set; }
        public string OutpatientNo { get; set; }
        public string ReservationTime { get; set; }
        public string SeeADoctorTime { get; set; }
        public string UserPhone { get; set; }
        public string ResultMsg { get; set; }
        internal JianLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"DoctorId - {DoctorId}");
            sb.AppendLine($"Dept2Code - {Dept2Code}");
            sb.AppendLine($"ReservationTime - {ReservationTime}");
            sb.AppendLine($"SeeADoctorTime - {SeeADoctorTime}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
