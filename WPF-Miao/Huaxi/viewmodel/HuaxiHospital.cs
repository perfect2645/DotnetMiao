using Base.viewModel.hospital;
using System.Text;

namespace Huaxi.viewmodel
{
    internal class HuaxiHospital : HospitalDept
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public new string Display
        {
            get { return $"{HospitalName}-{DepartmentName}"; }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********华西妇幼-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"Department : {DepartmentId} - {DepartmentName}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
