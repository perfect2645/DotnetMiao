using Base.Events;
using System;
using System.Text;

namespace Redhouse.appointment
{
    public class Order
    {
        public string PatientInfoId { get; set; }
        public string PatientName { get; set; }
        public int PatientSexFlag { get; set; }
        public int PatientMarryFlag { get; set; }
        public string PatientBirthDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int IdCardKind { get; set; }
        public string IdCardNo { get; set; }
        public int CardKind { get; set; }
        public string CardNo { get; set; }
        public string Phone { get; set; }
        public string PlanId { get; set; }
        public string ItemId { get; set; }
        public string CardId { get; set; }
        public string HosId { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"WechatId - {WechatId}");
            sb.AppendLine($"Time - {TimeRangeEncode}");
            sb.AppendLine($"PatBindId - {PatBindId}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
