using Base.viewModel.hospital;
using Jk160.constants;
using System.Text;
using Utils.stringBuilder;

namespace Jk160.viewmodel.platform
{
    internal class Jk160Dept : HospitalDept
    {
        public Jk160Dept(string platformId, string platName, string hosId, string hosName, string deptId, string deptName)
        {
            PlatformId = platformId;
            PlatformName = platName;
            HospitalId = hosId;
            HospitalName = hosName;
            DepartmentId = deptId;
            DepartmentName = deptName;
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********健康之路-选择医院科室*********");

            sb.AppendLine($"{HospitalName}-{DepartmentName}");
            sb.AppendField(Constant.HospitalId, HospitalId);
            sb.AppendField(Constant.DeptId, DepartmentId);

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
