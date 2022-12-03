using Base.viewModel.hospital;
using System.Text;

namespace gaoxin.viewmodel
{
    internal class GaoxinHospital : HospitalDept
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string AppointAmount { get; set; } = "0";

        public new string Display
        {
            get { return $"{DepartmentName}-{DoctorName}"; }
        }
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
