using HosTwo.login;
using System.Text;

namespace HosTwo.appointment
{
    public class Order
    {
        public string HisId { get; set; }
        public string PlatformId { get; set; }

        internal HosTwoLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {HospitalUserID}");
            sb.AppendLine($"RegistDate - {RegistDate}");
            sb.AppendLine($"DeptName - {DeptName}");
            sb.AppendLine($"BookingID - {BookingID}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
