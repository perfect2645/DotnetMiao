using Base.viewModel.hospital;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dxm.viewmodel
{
    internal class DxmHospital : HospitalDept
    {

        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSign { get; set; }

        public new string Display 
        {  
            get 
            { 
                return $"{HospitalName}-{DepartmentName}-{DoctorName}"; 
            }
        }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********大连2院-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine($"{DoctorName} - {DoctorId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
