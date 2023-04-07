using System.Text;

namespace Gangwu.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ResultCode { get; set; }
        public string ResultMsg { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"Age - {Age}");
            sb.AppendLine($"Phone - {Phone}");
            sb.AppendLine($"Email - {Email}");
            sb.AppendLine($"ResultCode - {ResultCode}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
