using Base.Events;
using System;
using System.Text;
using Utils.timerUtil;

namespace suiyang.appointment
{
    public class Order
    {
        public IntervalOnTime IntervalOnTime { get; private set; }
        public string Type { get; set; } = "UNDEFINED";
        public string BtCode { get; set; } = "F";
        public string AppointDate { get; set; }
        public string BeginTime { get; set; } = "08:00";
        public string EndTime { get; set; } = "16:30";
        public string OpenId { get; set; } = "ohcxP58icuLf35ZBbVsSB7mZ2YZ8";
        public string Barcode { get; set; }
        public int Price { get; set; } = 0;
        public int Priority { get; set; } = 10;
        public int AddressId { get; set; } = 61924;
        public string Identity { get; set; }
        public string Phone { get; set; }
        public string IdName { get; set; }

        public Order()
        {
            IntervalOnTime = new IntervalOnTime(Barcode, 600);
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约成功 *********");

            sb.AppendLine($"身份证 - {Identity}");
            sb.AppendLine($"姓名 - {IdName}");
            sb.AppendLine($"Phone - {Phone}");
            sb.AppendLine($"Barcode - {Barcode}");
            sb.AppendLine($"AppointDate - {AppointDate}");
            sb.AppendLine($"Time - {BeginTime}-{EndTime}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
