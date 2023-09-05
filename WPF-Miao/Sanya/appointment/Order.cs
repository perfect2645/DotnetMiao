using Sanya.login;
using System.Text;

namespace Sanya.appointment
{
    public class Order
    {
        public string UserName { get; set; }
        public string IdCardNo { get; set; }
        public string IcCardNo { get; set; }
        public string GoodsId { get; set; }
        public string OrgCode { get; set; }
        public string TimeStr { get; set; }
        public string ServiceId { get; set; }
        public string GoodsDetailId { get; set; }
        public string SubscribeType { get; set; }
        public string ProvideAddress { get; set; }
        public string Age { get; set; }

        public string ResultMsg { get; set; }

        internal SanyaLogin User { get; set; }

        public string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("******** 预约详情 *********");

            sb.AppendLine($"姓名 - {UserName}");
            sb.AppendLine($"IdCardNo - {IdCardNo}");
            sb.AppendLine($"Age - {Age}");
            sb.AppendLine($"Time - {TimeStr}");
            sb.AppendLine($"GoodsId - {GoodsId}");
            sb.AppendLine($"GoodsDetailId - {GoodsDetailId}");
            sb.AppendLine($"ResultMsg - {ResultMsg}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
