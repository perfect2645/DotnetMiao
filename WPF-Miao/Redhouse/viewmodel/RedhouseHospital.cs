using Base.viewModel.hospital;
using System.Text;

namespace Redhouse.viewmodel
{
    internal class RedhouseHospital : HospitalDept
    {
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********好医师-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
