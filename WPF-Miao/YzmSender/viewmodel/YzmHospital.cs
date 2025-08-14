using Base.viewModel.hospital;
using System.Text;

namespace YzmSender.viewmodel
{
    internal class YzmSenderHospital : HospitalDept
    {
        public string BaseUrl { get; set; }

        public new string Display
        {
            get { return $"{HospitalName}"; }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********YzmSender-选择医院科室*********");
            sb.AppendLine($"{HospitalName}");
            sb.AppendLine($"BaseUrl : {BaseUrl}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
