using Kuerle.login;
using System.Text;

namespace Kuerle.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string IDcard { get; set; }
        public string PID { get; set; }
        public string VID { get; set; }
        public string Date { get; set; }
        public string ResultMsg { get; set; }
        internal KuerleLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"IDcard - {IDcard}");
            sb.AppendLine($"PID - {PID}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
