using Base.Events;
using System;
using System.Text;
using Shishi.login;
using Utils.timerUtil;

namespace Shishi.appointment
{
    public class Order
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DutyTimeId { get; set; }
        public string VaccineId { get; set; }
        public string HosipitalId { get; set; }
        public string InoculateTimes { get; set; } //第几针
        public string SeeDate { get; set; }
        public string Address { get; set; } = "天河";
        internal ShishiLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"SeeDate - {SeeDate}");
            sb.AppendLine($"VaccineId - {VaccineId}");
            sb.AppendLine($"DutyTimeId - {DutyTimeId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
