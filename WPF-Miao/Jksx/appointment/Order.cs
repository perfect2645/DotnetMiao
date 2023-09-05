using Jksx.login;
using System.Text;

namespace Jksx.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string Hospitaluid { get; set; }
        public string Accesstype { get; set; }
        public string Resid { get; set; }
        public string Commonpeopleid { get; set; }
        public string Workid { get; set; }
        public string Treattype { get; set; }
        public string Mtype { get; set; }
        public string Userid { get; set; }
        public string Workdate { get; set; }
        

        public string ResultMsg { get; set; }
        internal JksxLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"{UserName} - {Userid}");
            sb.AppendLine($"Workdate - {Workdate}");
            sb.AppendLine($"Resid - {Resid}");
            sb.AppendLine($"Hospitaluid - {Hospitaluid}");
            sb.AppendLine($"Commonpeopleid - {Commonpeopleid}");
            sb.AppendLine($"Workid - {Workid}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
