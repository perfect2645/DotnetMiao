using Base.viewModel.hospital;
using System.Text;

namespace Xihongmen.viewmodel
{
    internal class XhmHospital : HospitalDept
    {
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********北京西红门-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
