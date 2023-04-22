using Tongzhou.login;
using System.Text;

namespace Tongzhou.appointment
{
    public class Order
    {
        public string HospitalUserID { get; set; }
        public string UserName { get; set; }
        public string HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string ResourceID { get; set; }
        public string Url { get; set; } = "https://fwcs.linkingcloud.cn/App/yuyue/index.html#/pages/yuyue/doctor?dataSource=";
        public string DocCode { get; set; }
        public string DocName { get; set; }
        public string DocDuty { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string RegistDate { get; set; }
        public string T { get; set; } = "1";
        public string BookingID { get; set; }
        internal TongzhouLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {HospitalUserID}");
            sb.AppendLine($"RegistDate - {RegistDate}");
            sb.AppendLine($"DeptName - {DeptName}");
            sb.AppendLine($"BookingID - {BookingID}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
