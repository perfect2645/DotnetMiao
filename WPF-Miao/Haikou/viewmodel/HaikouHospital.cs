using Base.viewModel.hospital;
using System.Text;

namespace Haikou.viewmodel
{
    internal class HaikouHospital : HospitalDept
    {
        public string AppId { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public new string Display
        {
            get { return $"{DepartmentName}-{ServiceName}"; }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********武汉疾控-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"Department : {DepartmentId} - {DepartmentName}");
            sb.AppendLine($"Service : {ServiceCode} - {ServiceName}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
