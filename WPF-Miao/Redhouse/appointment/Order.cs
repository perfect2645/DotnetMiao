using Base.Events;
using System;
using System.Text;

namespace Redhouse.appointment
{
    public class Order
    {
        public string EncryKey { get; set; }
        public string Data { get; set; }
        public string Sign { get; set; }
        public string Time { get; set; }
        public string TimeRangeEncode { get; set; }
        public string WechatId { get; set; }
        public string PatBindId { get; set; }
        public string OrgId { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"WechatId - {WechatId}");
            sb.AppendLine($"Time - {TimeRangeEncode}");
            sb.AppendLine($"PatBindId - {PatBindId}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
