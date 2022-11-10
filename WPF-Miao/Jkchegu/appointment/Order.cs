using System;
using System.Collections.Generic;
using System.Text;
using Utils.stringBuilder;

namespace Jkchegu.appointment
{
    public class Order
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Yzm { get; set; }
        public string GUID { get; set; }

        public Dictionary<string, object> Doccustom = new Dictionary<string, object>();

        public Order(string date, string time, Dictionary<string, object> doccustom)
        {
            Date = date;
            Time = time;
            Doccustom = doccustom;

            BuildOrder(doccustom);
        }

        private void BuildOrder(Dictionary<string, object> doccustom)
        {
            GUID = doccustom["GUID"].NotNullString();
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");

            sb.AppendLine($"Date - {Date}");
            sb.AppendLine($"Time - {Time}");
            sb.AppendLine($"Guid - {GUID}");
            sb.AppendLine($"Yzm - {Yzm}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
