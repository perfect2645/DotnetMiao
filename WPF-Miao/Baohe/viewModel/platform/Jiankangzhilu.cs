using Baohe.constants;
using Base.viewModel.hospital;
using System.Text;
using Utils.stringBuilder;

namespace Baohe.viewModel.platform
{
    internal class Jiankangzhilu : HospitalDept
    {
        public bool HasYzm { get; set; }
        public string DoctorSn { get; set; }
        public Jiankangzhilu(string platformId, string platName, string hosId, string hosName, string deptId, string deptName)
        {
            PlatformId = platformId;
            PlatformName = platName;
            HospitalId = hosId;
            HospitalName = hosName;
            DepartmentId = deptId;
            DepartmentName = deptName;
            HasYzm = true;
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********健康之路-选择医院科室*********");

            sb.AppendLine($"{HospitalName}-{DepartmentName}");
            sb.AppendField(Constant.PlatformType, PlatformId);
            sb.AppendField(Constant.HospitalId, HospitalId);
            sb.AppendField(Constant.DeptId, DepartmentId);

            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
