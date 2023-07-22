using SixWater.login;
using System.Text;

namespace SixWater.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string DeptId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorScheduleId { get; set; }
        public string ScheduleDate { get; set; }
        public string OrgId { get; set; }
        //public string OrderNumber { get; set; }
        public string RegisterTypeId { get; set; }
        public string TotalFee { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string Emergency { get; set; }
        public string RegisterCategory { get; set; }
        public string FamilyMemberId { get; set; }
        public string JsonContent { get; set; }
        public string OrderId { get; set; }
        public string ResultMsg { get; set; }
        internal SixWaterLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"{UserName} - {FamilyMemberId}");
            sb.AppendLine($"doctorScheduleId - {DoctorScheduleId}");
            sb.AppendLine($"{ScheduleDate}");
            sb.AppendLine($"{BeginTime} - {EndTime}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
