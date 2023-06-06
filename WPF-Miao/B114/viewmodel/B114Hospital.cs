using Base.viewModel.hospital;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace B114.viewmodel
{
    internal class B114Hospital : HospitalDept
    {
        public string AreaId { get; set; }

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

            sb.AppendLine("********B114-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
