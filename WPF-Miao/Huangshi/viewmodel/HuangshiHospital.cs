using Base.viewModel.hospital;
using System.Text;

namespace Huangshi.viewmodel
{
    internal class HuangshiHospital : HospitalDept
    {

        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string PackageId { get; set; }

        public new string Display
        {
            get { return $"{DepartmentName}-{DoctorName}"; }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********黄石妇幼-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"Department : {DepartmentId} - {DepartmentName}");
            sb.AppendLine($"Doctor : {DoctorId} - {DoctorName}");
            sb.AppendLine($"PackageId : {PackageId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
