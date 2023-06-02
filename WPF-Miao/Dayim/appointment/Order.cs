using Dayim.login;
using System.Text;

namespace Dayim.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string StockId { get; set; }
        public string VaccinateTimeDetailId { get; set; }
        public string EntCompanyId { get; set; }
        public string VacId { get; set; }
        public string VacTypeId { get; set; }
        public string QueueId { get; set; }
        public string ReserveDate { get; set; }
        public string ResultMsg { get; set; }

        internal DayimLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"StockId - {StockId}");
            sb.AppendLine($"ReserveDate - {ReserveDate}");
            sb.AppendLine($"VaccinateTimeDetailId - {VaccinateTimeDetailId}");
            sb.AppendLine($"EntCompanyId - {EntCompanyId}");
            sb.AppendLine($"VacId - {VacId}");
            sb.AppendLine($"VacTypeId - {VacTypeId}");
            sb.AppendLine($"QueueId - {QueueId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
