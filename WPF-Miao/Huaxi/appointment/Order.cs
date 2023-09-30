using Huaxi.login;
using System.Text;

namespace Huaxi.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string OrganCode { get; set; }
        public string AppCode { get; set; }
        public string ChannelCode { get; set; }
        public string CardId { get; set; }
        public string ScheduleId { get; set; }
        public string SureOrderVerify { get; set; }
        public string SureOrderVerifyInfo { get; set; }
        public string SureOrderVerifyIndex { get; set; }
        public string VerifyImageCodeType { get; set; }
        public string OrderId { get; set; }
        public string ResultMsg { get; set; }
        internal HuaxiLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"OrganCode - {OrganCode}");
            sb.AppendLine($"ChannelCode - {ChannelCode}");
            sb.AppendLine($"ScheduleId - {ScheduleId}");
            sb.AppendLine($"SureOrderVerify - {SureOrderVerify}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
