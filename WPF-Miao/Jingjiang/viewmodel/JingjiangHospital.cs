using Base.viewModel.hospital;
using System.Text;

namespace Jingjiang.viewmodel
{
    internal class JingjiangHospital : HospitalDept
    {
        public string DwCode { get; set; }

        public new string Display
        {
            get { return $"{DepartmentName}"; }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********武汉疾控-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DwCode : {DwCode}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
