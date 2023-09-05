using Lujiazhen.login;
using System.Text;

namespace Lujiazhen.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string VaccineDayId { get; set; }
        public string VaccineDayNumId { get; set; }
        public string VaccineId { get; set; }
        public string FamilyId { get; set; }
        public string ResultMsg { get; set; }
        internal LujiazhenLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"FamilyId - {FamilyId}");
            sb.AppendLine($"VaccineDayId - {VaccineDayId}");
            sb.AppendLine($"VaccineId - {VaccineId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
