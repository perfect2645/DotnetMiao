using B114.login;
using System.Text;

namespace B114.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string UserId { get; set; }

        public string HosCode { get; set; }
        public string FirstDeptCode { get; set; }
        public string SecondDeptCode { get; set; }
        public string TreatmentDay { get; set; }
        public string UniqProductKey { get; set; }
        public string CardType { get; set; }
        public string CardNo { get; set; }
        public string SmsCode { get; set; }
        public string JytCardId { get; set; }
        public string HospitalCardId { get; set; }
        public string Phone { get; set; }
        public string DutyTime { get; set; }
        public string OrderFrom { get; set; }
        public string OrderNo { get; set; }

        public string ResultMsg { get; set; }

        internal B114Login User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"Phone - {Phone}");
            sb.AppendLine($"SecondDeptCode - {SecondDeptCode}");
            sb.AppendLine($"TreatmentDay - {TreatmentDay}");
            sb.AppendLine($"DutyTime - {DutyTime}");
            sb.AppendLine($"UniqProductKey - {UniqProductKey}");
            sb.AppendLine($"OrderNo - {OrderNo}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
