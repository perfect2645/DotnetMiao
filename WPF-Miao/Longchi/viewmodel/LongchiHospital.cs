using Base.viewModel.hospital;
using System.Collections.Generic;
using System.Text;

namespace Longchi.viewmodel
{
    internal class LongchiHospital : HospitalDept
    {
        public List<string> DepartmentList { get; set; } = new List<string>();
        public List<string> TimeIdList { get; set; } = new List<string>();
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********龙池-选择医院科室*********");
            sb.AppendLine($"{HospitalName}");
            sb.AppendLine($"{DepartmentName}");
            sb.AppendLine($"{string.Join(",", DepartmentList)}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
