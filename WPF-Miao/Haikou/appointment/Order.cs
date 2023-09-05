using Haikou.login;
using System.Text;

namespace Haikou.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string PatientId { get; set; }
        public string ScheduleId { get; set; }
        public string Appid { get; set; }
        public string Token { get; set; }
        public string DeptName { get; set; }
        public string DateTime { get; set; }
        public string OrderId { get; set; }
        public string ResultMsg { get; set; }
        internal HaikouLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"PatientId - {PatientId}");
            sb.AppendLine($"DeptName - {DeptName}");
            sb.AppendLine($"DateTime - {DateTime}");
            sb.AppendLine($"ScheduleId - {ScheduleId}");
            sb.AppendLine($"OrderId - {OrderId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
