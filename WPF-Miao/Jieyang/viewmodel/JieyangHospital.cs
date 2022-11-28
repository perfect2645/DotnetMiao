using Base.viewModel.hospital;
using System.Text;

namespace jieyang.viewmodel
{
    internal class JieyangHospital : HospitalDept
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********揭阳安贞-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine($"Doctor - {DoctorName}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
