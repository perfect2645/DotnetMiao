using System.Text;

namespace Tjzs.appointment
{
    public class Order
    {
        public string Authorization { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string IdCard { get; set; }
        public string ResultCode { get; set; }
        public string ResultMsg { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserId}- {UserName}");
            sb.AppendLine($"Phone - {Phone}");
            sb.AppendLine($"IdCard - {IdCard}");
            sb.AppendLine($"ResultCode - {ResultCode}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
