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
        public string ResultMsg { get; set; }
        //public string Zxx { get; set; }
        internal JingjiangLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"身份证 - {Sfz}");
            sb.AppendLine($"phone - {Sjhm}");
            sb.AppendLine($"YwId - {YwId}");
            sb.AppendLine($"YwDateId - {YwDateId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
