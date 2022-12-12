using Baohe.constants;
using Utils;

namespace Baohe.appointment.content
{
    internal class JinggangContent : AppointmentContent
    {
        public JinggangContent() : base()
        {

        }
        protected override void BuildDefaultDoctorOrder()
        {
            base.BuildDefaultDoctorOrder();
            DoctorOrder.AddOrUpdate("birthday", MemberInfo["Birthday"]);
            DoctorOrder.AddOrUpdate("timeId", 1);
        }
        protected override void BuildDefaultGhFormCon()
        {
            base.BuildDefaultGhFormCon();
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Familyaddress"], "familyaddress"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Cliniccard"], "ClinicCard"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Birthday"], "birthday"));
            GhFormConOrder.Add(BuildGhFormConItem("1", "isread"));
        }

        protected override void BuildNumberDoctorOrder()
        {
            base.BuildNumberDoctorOrder();
            DoctorOrder.AddOrUpdate(Constant.WaterId, MiaoInfo["NumberSN"]);
        }
    }
}
