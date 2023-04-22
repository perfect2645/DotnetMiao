using Base.viewModel.hospital;
using System.Text;

namespace Tongzhou.viewmodel
{
    internal class TongzhouHospital : HospitalDept
    {

        public string DocCode { get; set; }
        public string DocName { get; set; }
        public string DocDuty { get; set; }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********上海4院-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine($"{DocName} - {DocCode}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
