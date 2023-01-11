using System.Text;
using Utils.timerUtil;
using Xihongmen.login;

namespace Xihongmen.appointment
{
    public class Order
    {
        public IntervalOnTime IntervalOnTime { get; private set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string TypeId { get; set; }
        public string TypeTitle { get; set; }
        public string Date { get; set; }
        public string TimeType { get; set; }
        public string OrderId { get; set; }
        internal XhmLogin User { get; set; }
        internal string Token { get; set; }

        public Order()
        {
            IntervalOnTime = new IntervalOnTime(Date, 600);
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约信息 *********");

            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"UserName - {UserName}");
            sb.AppendLine($"Date - {Date}");
            sb.AppendLine($"TimeType - {TimeType}");
            sb.AppendLine($"OrderId - {OrderId}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
