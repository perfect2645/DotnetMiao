using Gzjk.login;
using System.Text;

namespace Gzjk.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string Birthday { get; set; }
        public string Tel { get; set; }
        public int Sex { get; set; } = 1;
        public int Doctype { get; set; } = 1;
        public string Idcard { get; set; }
        public string Mxid { get; set; }
        public string Date { get; set; }
        public string Pid { get; set; }
        public int Ftime { get; set; } = 1; //第几针
        public string Guid { get; set; }

        public string ResultMsg { get; set; }

        internal GzjkLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"FamilyId - {FamilyId}");
            sb.AppendLine($"Phone - {Phone}");
            sb.AppendLine($"IdCard - {IdCard}");
            sb.AppendLine($"Time - {Time}");
            sb.AppendLine($"SchId - {SchId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
