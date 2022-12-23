using Base.Events;
using System;
using System.Text;
using Utils.timerUtil;

namespace Jingyang.appointment
{
    public class Order
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string TimeId { get; set; }
        public string Fid { get; set; }
        public string Yid { get; set; }
        public string Relation { get; set; } = "1";
        public string Token { get; set; }
        public string Cookie { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"TimeId - {TimeId}");
            sb.AppendLine($"Fid - {Fid}");
            sb.AppendLine($"Yid - {Yid}");
            sb.AppendLine($"Token - {Token}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
