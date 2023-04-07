using Baohe.constants;
using Utils;

namespace Baohe.appointment.content
{
    internal class NangangContent : AppointmentContent
    {
        public NangangContent() : base()
        {

        }
        protected override void BuildDefaultDoctorOrder()
        {
            base.BuildDefaultDoctorOrder();
            DoctorOrder.AddOrUpdate("birthday", MemberInfo["Birthday"]);
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
            DoctorOrder.AddOrUpdate(Constant.WaitingInfor, $"{MiaoInfo["CommendScope"]}");
        }
    }
}
