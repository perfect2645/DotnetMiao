using HosTwo.login;
using System.Text;

namespace HosTwo.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string HisId { get; set; }
        public string PlatformId { get; set; }
        public string PlatformSource { get; set; }
        public string SubSource { get; set; }
        public string SubHisId { get; set; }
        public string DeptId { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string SearchMonth { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleId { get; set; }
        public string VisitPeriod { get; set; }
        public string VisitBeginTime { get; set; }
        public string VisitEndTime { get; set; }
        public string Token { get; set; }
        public string ResultMsg { get; set; }

        internal HosTwoLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"PatientId - {PatientId}");
            sb.AppendLine($"ScheduleDate - {ScheduleDate}");
            sb.AppendLine($"Time - {VisitBeginTime}-{VisitEndTime}");
            sb.AppendLine($"HisId - {HisId}");
            sb.AppendLine($"DeptId - {DeptId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
