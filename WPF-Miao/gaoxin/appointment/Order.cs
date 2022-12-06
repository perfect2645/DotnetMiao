using Base.Events;
using gaoxin.session;
using System;
using System.Text;
using Utils.timerUtil;

namespace gaoxin.appointment
{
    public class Order
    {
        public IntervalOnTime IntervalOnTime { get; private set; }
        public string daid { get; set; }
        public string UserName { get; set; }
        public string yysj { get; set; }
        public string yysjd { get; set; }
        public string yyymid { get; set; }
        public string disparkId { get; set; }
        public int yyjg { get; set; }
        public string jgid { get; set; }
        public string jgcommid { get; set; }

        public Order()
        {
            if (MainSession.Interval < 50)
            {
                MainSession.Interval = 200;
            }
            IntervalOnTime = new IntervalOnTime(daid, MainSession.Interval);
        }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 开始预约 *********");
            sb.AppendLine($"UserName - {UserName}");
            sb.AppendLine($"UserId - {daid}");
            sb.AppendLine($"Date - {yysj}");
            sb.AppendLine($"Time - {yysjd}");
            sb.AppendLine($"yyymid - {yyymid}");
            sb.AppendLine($"disparkId - {disparkId}");
            sb.AppendLine($"yyjg - {yyjg}");
            sb.AppendLine($"jgid - {jgid}");
            sb.AppendLine($"jgcommid - {jgcommid}");

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
