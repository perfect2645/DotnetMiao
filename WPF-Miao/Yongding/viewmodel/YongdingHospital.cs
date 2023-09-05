using Base.viewModel.hospital;
using System.Text;

namespace Yongding.viewmodel
{
    internal class YongdingHospital : HospitalDept
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

            sb.AppendLine("********武汉疾控-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"Department : {DepartmentId} - {DepartmentName}");
            sb.AppendLine($"Doctor : {DoctorId} - {DoctorName}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
