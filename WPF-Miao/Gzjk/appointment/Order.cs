using Puzhou.login;
using System.Text;

namespace Puzhou.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string FamilyId { get; set; }
        public string HosId { get; set; }
        public string IdCard { get; set; }
        public string NumId { get; set; }
        public string OpenId { get; set; }
        public string Phone { get; set; }
        public string ProjectId { get; set; }
        public string SchId { get; set; }
        public string Time { get; set; }

        public string ResultMsg { get; set; }

        internal GzjkLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"FamilyId - {FamilyId}");
            sb.AppendLine($"Phone - {Phone}");
            sb.AppendLine($"IdCard - {IdCard}");
            sb.AppendLine($"Time - {Time}");
            sb.AppendLine($"SchId - {SchId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
