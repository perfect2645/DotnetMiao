using Base.viewModel.hospital;
using System.Text;

namespace Kuerle.viewmodel
{
    internal class KuerleHospital : HospitalDept
    {

        public string Keywords { get; set; }
        public new string Display
        {
            get { return $"{DepartmentName}- {Keywords}"; }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********库尔勒疾控-选择医院科室*********");
            sb.AppendLine($"{HospitalName}");
            sb.AppendLine($"Department : {DepartmentName} - {DepartmentName}");
            sb.AppendLine($"Doctor : {Keywords} - {Keywords}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
