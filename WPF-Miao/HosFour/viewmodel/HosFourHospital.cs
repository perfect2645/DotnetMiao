using Base.viewModel.hospital;
using System.Text;

namespace HosFour.viewmodel
{
    internal class HosFourHospital : HospitalDept
    {
        public string HospitalPrefix { get; set; }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********上海4院-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalPrefix}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
