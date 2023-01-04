using Base.Events;
using System;
using System.Text;
using Utils.timerUtil;

namespace Tianhe.appointment
{
    public class Order
    {
        public string UserId { get; set; }
        public string FamilyId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string Qty { get; set; } = "1";
        public string Dizhi { get; set; }
        public string YuyueName { get; set; }
        public string YuyueUserAdd { get; set; }
        public string YuyueUserSuoshu { get; set; }
        public string Cookie { get; set; }
        public string ReturnId { get; set; }
        public string OrderId { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"FamilyId - {FamilyId}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"Date - {Date}");
            sb.AppendLine($"Dizhi - {Dizhi}");
            sb.AppendLine($"YuyueName - {YuyueName}");
            sb.AppendLine($"ReturnId - {ReturnId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
