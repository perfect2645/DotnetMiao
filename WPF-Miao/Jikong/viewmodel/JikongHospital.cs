using Base.viewModel.hospital;
using System.Text;

namespace Jikong.viewmodel
{
    internal class JikongHospital : HospitalDept
    {

        public string DoctorId { get; set; }
        public string DoctorName { get; set; }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********武汉疾控-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
