using Dalian.login;
using System.Text;

namespace Dalian.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string UserId { get; set; }

        public string PointId { get; set; }
        public string PointName { get; set; }
        public string PointDate { get; set; }
        public string RegLevelId { get; set; }
        public string RegLevelName { get; set; }
        public string DeptId { get; set; }
        public string DrId { get; set; }
        public string VisitTime { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string DiagnoseFee { get; set; } = "8.00";
        public string NoonId { get; set; }
        public string NoonName { get; set; }
        public string HospId { get; set; }
        public string OrderNo { get; set; }

        public string ResultMsg { get; set; }

        internal DalianLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"PointId - {PointId}");
            sb.AppendLine($"RegLevelName - {RegLevelName}");
            sb.AppendLine($"PointName - {PointName}");
            sb.AppendLine($"PointDate - {PointDate}");
            sb.AppendLine($"VisitTime - {VisitTime}");
            sb.AppendLine($"Time - {BeginTime}-{EndTime}");
            sb.AppendLine($"VisitTime - {VisitTime}");
            sb.AppendLine($"OrderNo - {OrderNo}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
