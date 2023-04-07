using Base.viewModel.hospital;
using System.Text;

namespace chutian.viewmodel
{
    internal class ChutianHospital : HospitalDept
    {
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********楚天名医-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
