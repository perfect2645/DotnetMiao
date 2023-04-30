using Base.viewModel.hospital;
using System.Text;

namespace Tongzhou.viewmodel
{
    internal class TongzhouHospital : HospitalDept
    {

        public string DoctorId { get; set; }
        public string DoctorName { get; set; }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********上海4院-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine($"{DoctorName} - {DoctorId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
