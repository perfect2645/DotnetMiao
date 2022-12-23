using Base.viewModel.hospital;
using System.Text;

namespace Jingyang.viewmodel
{
    internal class JingyangHospital : HospitalDept
    {
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********旌阳-选择医院科室*********");
            sb.AppendLine($"{HospitalName}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
