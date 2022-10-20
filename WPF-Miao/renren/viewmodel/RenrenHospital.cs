using Base.viewModel.hospital;
using renren.session;
using System.Text;
using Utils.stringBuilder;

namespace renren.viewmodel
{
    internal class RenrenHospital : HospitalDept
    {
        public string UserHospitalId { get; set; }

        public override string ToLogString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("********仁仁医疗-选择医院科室*********");
            sb.AppendLine($"{HospitalName}- {DepartmentName}");
            sb.AppendField(Constants.HospitalId, HospitalId);
            sb.AppendField(Constants.UserHospitalId, UserHospitalId);
            sb.AppendLine("**************************************");

            return sb.ToString();
        }
    }
}
