using Base.viewModel.hospital;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gzjk.viewmodel
{
    internal class GzjkHospital : HospitalDept
    {
        public new string Display 
        {  
            get 
            { 
                return $"{HospitalName}-{DepartmentId}-{DepartmentName}"; 
            }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********Gzjk-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
