using Jingjiang.login;
using System.Text;

namespace Jingjiang.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string YwId { get; set; }
        public string YwDateId { get; set; }
        public string DatetimeId { get; set; }
        public string Sfz { get; set; }
        public string Sjhm { get; set; }
        public string DwCode { get; set; }
        //public string Zxx { get; set; }
        internal JingjiangLogin User { get; set; }

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
