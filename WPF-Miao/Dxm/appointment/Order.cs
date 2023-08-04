using Dxm.login;
using System.Text;

namespace Dxm.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string MakeAnAppointment { get; set; }
        public string TimeNo { get; set; }
        public string TimeType { get; set; }
        public string OrderId { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
        public string FacName { get; set; }
        public string Spec { get; set; }
        public string Price { get; set; }
        public string PriceTxt { get; set; }
        public string VaccineType { get; set; }
        public string BeginCountTime { get; set; }
        public string Address { get; set; }
        public string HospitalCode { get; set; }
        public string ResultMsg { get; set; }

        internal DxmLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"UserId - {UserId}");
            sb.AppendLine($"Date - {MakeAnAppointment}");
            sb.AppendLine($"Time - {TimeNo}");
            sb.AppendLine($"HospitalCode - {HospitalCode}");
            sb.AppendLine($"{DeptId} - {DeptName}");
            sb.AppendLine($"OrderId - {OrderId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
