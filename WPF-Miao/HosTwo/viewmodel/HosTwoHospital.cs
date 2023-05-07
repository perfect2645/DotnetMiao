using Base.viewModel.hospital;
using System.Text;

namespace HosTwo.viewmodel
{
    internal class HosTwoHospital : HospitalDept
    {

        public string DocCode { get; set; }
        public string DocName { get; set; }
        public string DocDuty { get; set; }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********大连2院-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine($"{DocName} - {DocCode}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
