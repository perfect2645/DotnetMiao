using Base.viewModel.hospital;
using System.Text;

namespace suiyang.viewmodel
{
    internal class SuiyangHospital : HospitalDept
    {
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********绥阳-选择医院科室*********");
            sb.AppendLine($"{HospitalName}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
