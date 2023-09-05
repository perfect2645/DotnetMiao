using Base.viewModel.hospital;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shangyu.viewmodel
{
    internal class ShangyuHospital : HospitalDept
    {

        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
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

            sb.AppendLine("********吉安市妇幼保健院-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {HospitalId}");
            sb.AppendLine($"{DepartmentName} - {DepartmentId}");
            sb.AppendLine($"{DoctorName} - {DoctorId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
