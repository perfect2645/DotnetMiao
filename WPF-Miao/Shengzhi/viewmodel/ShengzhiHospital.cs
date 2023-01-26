using Base.viewModel.hospital;
using System.Text;

namespace Shengzhi.viewmodel
{
    internal class ShengzhiHospital : HospitalDept
    {
        private string _doctorId;
        public string DoctorId
        {
            get { return _doctorId; }
            set
            {
                _doctorId = value;
                NotifyUI(() => DoctorId);
            }
        }

        private string _doctorName;
        public string DoctorName
        {
            get { return _doctorName; }
            set
            {
                _doctorName = value;
                NotifyUI(() => DoctorName);
            }
        }


        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********河北生殖妇产医院-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendLine($"DepartmentId - {DepartmentId}");
            sb.AppendLine($"{HospitalName} - {HospitalId}");
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
