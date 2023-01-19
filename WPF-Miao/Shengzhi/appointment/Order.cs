using Base.Events;
using System;
using System.Text;
using Shengzhi.login;
using Utils.timerUtil;

namespace Shengzhi.appointment
{
    public class Order
    {
        public string AppointScore { get; set; }
        public string AppUuid { get; set; }
        public string ChannelId { get; set; }
        public string GroupCode { get; set; }
        public string HospitalWxOpenId { get; set; }
        public string ImeiId { get; set; }
        public string PhoneOperationSys { get; set; }
        public string PhoneType { get; set; }
        public string PhoneVersionNum { get; set; }
        public string PublicServiceType { get; set; }
        public string QyCheckSuffix { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserVsId { get; set; }
        public string HospitalId { get; set; }
        public string MonitorRecords { get; set; }
        public string OpVersion { get; set; }
        public string OperateCurrentUserId { get; set; }
        public string OperateUserSource { get; set; }
        internal ShengzhiLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            //sb.AppendLine($"SeeDate - {SeeDate}");
            //sb.AppendLine($"VaccineId - {VaccineId}");
            //sb.AppendLine($"DutyTimeId - {DutyTimeId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
