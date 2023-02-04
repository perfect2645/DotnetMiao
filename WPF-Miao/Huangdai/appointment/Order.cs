using System.Text;

namespace Huangdai.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string No { get; set; }
        public string ResultCode { get; set; }
        public string ResultMsg { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"Phone - {Phone}");
            sb.AppendLine($"Address - {Address}");
            sb.AppendLine($"Type - {Type}");
            sb.AppendLine($"No - {No}");
            sb.AppendLine($"ResultCode - {ResultCode}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
