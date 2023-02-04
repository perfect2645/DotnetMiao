using Huangdai.login;
using System.Text;

namespace Huangdai.appointment
{
    public class Order
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ItemName { get; set; }
        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string AmOrPm { get; set; }
        public string ScheduleCode { get; set; } //第几针
        public string ScheduleInfoCode { get; set; }
        public string Type { get; set; } = "1";
        internal HuangdaiLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"ItemName - {ItemName}");
            sb.AppendLine($"VisitDate - {VisitDate}");
            sb.AppendLine($"VisitTime - {VisitTime}");
            sb.AppendLine($"AmOrPm - {AmOrPm}");
            sb.AppendLine($"ScheduleCode - {ScheduleCode}");
            sb.AppendLine($"ScheduleInfoCode - {ScheduleInfoCode}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
