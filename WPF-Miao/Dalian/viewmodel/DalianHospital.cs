using Base.viewModel.hospital;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dalian.viewmodel
{
    internal class DalianHospital : HospitalDept
    {
        public string AppId { get; set; }
        public string HisDeptId { get; set; }
        public string RegLevelId { get; set; }
        public string RegLevelName { get; set; }

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

            sb.AppendLine("********Dalian-选择医院科室*********");
            sb.AppendLine($"{HospitalId} - {HospitalName}");
            sb.AppendLine($"{DepartmentId} - {DepartmentName}");
            sb.AppendLine($"RegLevelId - {RegLevelId}");
            sb.AppendLine($"RegLevelName - {RegLevelName}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
