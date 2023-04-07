using Base.viewModel.hospital;
using System.Collections.Generic;
using System.Text;

namespace Jingyang.viewmodel
{
    internal class JingyangHospital : HospitalDept
    {
        public List<string> TimeIdList { get; set; } = new List<string>();
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********旌阳-选择医院科室*********");
            sb.AppendLine($"{HospitalName}");
            sb.AppendLine($"{DepartmentName}");
            sb.AppendLine($"{string.Join(",", TimeIdList)}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
