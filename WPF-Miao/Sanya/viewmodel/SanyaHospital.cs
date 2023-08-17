using Base.viewModel.hospital;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sanya.viewmodel
{
    internal class SanyaHospital : HospitalDept
    {
        public string OrgCode { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string AppCode { get; set; }
        public string SubscribeType { get; set; }

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

            sb.AppendLine("********Sanya-选择医院科室*********");
            sb.AppendLine($"OrgCode - {OrgCode}");
            sb.AppendLine($"AppCode - {AppCode}");
            sb.AppendLine($"Zone - {ZoneCode} - {ZoneName}");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
