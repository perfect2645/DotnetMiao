using Kuerle.login;
using System.Text;

namespace Kuerle.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string Ids { get; set; }
        public string Uid { get; set; }
        public string ResultMsg { get; set; }
        internal KuerleLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {Uid}");
            sb.AppendLine($"Ids - {Ids}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
