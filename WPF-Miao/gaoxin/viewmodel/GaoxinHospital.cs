using Base.viewModel.hospital;
using System.Text;

namespace gaoxin.viewmodel
{
    internal class GaoxinHospital : HospitalDept
    {
        public string AppointAmount { get; set; } = "0";

        public new string Display
        {
            get { return $"{HospitalName}- {DepartmentName}"; }
        }
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********成都高新-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DisparkId - {DepartmentId}");
            sb.AppendLine($"DepartmentName - {DepartmentName}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
