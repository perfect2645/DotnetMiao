using Base.viewModel.hospital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuSheng.viewmodel
{
    internal class HushengHospital : HospitalDept
    {
        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********呼声-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
