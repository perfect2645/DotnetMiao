using Base.viewModel.hospital;
using System.Text;

namespace Zhi.viewmodel
{
    internal class ZhiHospital : HospitalDept
    {

        public new string Display
        {
            get { return $"{DepartmentId}-{DepartmentName}"; }
        }
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********苏州市相城区阳澄湖人民医院-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
