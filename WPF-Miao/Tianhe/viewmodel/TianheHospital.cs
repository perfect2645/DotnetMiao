using Base.viewModel.hospital;
using System.Text;

namespace Tianhe.viewmodel
{
    internal class TianheHospital : HospitalDept
    {
        public string HospitalPrefix { get; set; }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********天河-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalPrefix}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
