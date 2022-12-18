using Base.viewModel.hospital;
using System.Text;

namespace Ych.viewmodel
{
    internal class YchHospital : HospitalDept
    {

        public new string Display
        {
            get { return $"{DepartmentId}-{DepartmentName}"; }
        }
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********武汉金银湖社区-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
