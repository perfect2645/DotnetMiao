using Yongding.login;
using System.Text;

namespace Yongding.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string Id { get; set; }
        public string Riqi { get; set; }
        public string Timeid { get; set; }
        public string Time { get; set; }
        public string No { get; set; }
        public string Cardid { get; set; }
        public string Token { get; set; }
        internal YongdingLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"Id - {Id}");
            sb.AppendLine($"Date - {Riqi}-{Time}");
            sb.AppendLine($"Number - {No}");
            sb.AppendLine($"Cardid - {Cardid}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
