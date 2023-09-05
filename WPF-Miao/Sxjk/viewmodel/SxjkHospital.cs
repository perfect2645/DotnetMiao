using Base.viewModel.hospital;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sxjk.viewmodel
{
    internal class SxjkHospital : HospitalDept
    {

        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StationCode { get; set; }
        public string StationName { get; set; }

        public new string Display 
        {  
            get 
            { 
                return $"{CityName}-{HospitalName}-{DepartmentName}"; 
            }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********Sxjk-选择医院科室*********");
            sb.AppendLine($"{CityCode}- {CityName}");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
