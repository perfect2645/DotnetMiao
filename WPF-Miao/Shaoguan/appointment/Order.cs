using Shaoguan.login;
using System.Text;

namespace Shaoguan.appointment
{
    public class Order
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Deptid { get; set; }
        public string DeptName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string RegDate { get; set; }
        public string TimeID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public long TimeStamp { get; set; }
        public string PaymentType { get; set; }
        public string PatientId { get; set; }
        public string Token { get; set; }
        public string PaymentNo { get; set; }

        internal ShaoguanLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"DeptName - {DeptName}");
            sb.AppendLine($"TimeID - {TimeID}");
            sb.AppendLine($"PatientId - {PatientId}");
            sb.AppendLine($"PaymentNo - {PaymentNo}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
