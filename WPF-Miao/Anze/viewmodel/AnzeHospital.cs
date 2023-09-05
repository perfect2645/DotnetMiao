using Base.viewModel.hospital;
using System.Text;

namespace Anze.viewmodel
{
    internal class AnzeHospital : HospitalDept
    {

        public string DoctorId { get; set; }
        public string DoctorName { get; set; }

        public new string Display
        {
            get { return $"{DepartmentName}-{DoctorName}"; }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********安泽疾控-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"Department : {DepartmentId} - {DepartmentName}");
            sb.AppendLine($"Doctor : {DoctorId} - {DoctorName}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
