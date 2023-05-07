using Tongzhou.login;
using System.Text;

namespace Tongzhou.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string MpiId { get; set; }
        public string OrganAppointId { get; set; }
        public int ScheduleId { get; set; }
        public string ScheduleTimeId { get; set; }
        public string OrderNumSopt { get; set; }
        public string OrganId { get; set; }
        public string AppointDepartId { get; set; }
        public string AppointDepartName { get; set; }
        public string DoctorId { get; set; }
        public string WorkDate { get; set; }
        public int WorkType { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int OrderNum { get; set; }
        public int AppointRoad { get; set; }
        public int AppointStatus { get; set; }
        public int AppointPath { get; set; }
        public int AppointUser { get; set; }
        public string AppointName { get; set; }
        public string AppointOragn { get; set; }
        public int ClinicPrice { get; set; }
        public int TransferId { get; set; }
        public int SourceLevel { get; set; }
        public string ClinicId { get; set; }
        public int IfCreateFollowPlan { get; set; }
        public string CardId { get; set; }
        public string TriggerId { get; set; }
        public string AnalyzeNvcData { get; set; }
        public string RuleString { get; set; }
        public int IsRealTime { get; set; }
        public string CardType { get; set; }
        public string IllSummaryTxt { get; set; }
        public string ThirdChannel { get; set; }

        internal TongzhouLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName} - {AppointUser}");
            sb.AppendLine($"MpiId - {MpiId}");
            sb.AppendLine($"DeptName - {AppointDepartName}");
            sb.AppendLine($"StartTime - {StartTime}");
            sb.AppendLine($"EndTime - {EndTime}");
            sb.AppendLine($"OrderNum - {OrderNum}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
