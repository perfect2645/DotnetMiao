using Baohe.constants;
using Utils;
using Utils.number;

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
        }
        protected override void BuildDefaultGhFormCon()
        {
            base.BuildDefaultGhFormCon();
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Familyaddress"], "familyaddress"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Cliniccard"], "ClinicCard"));
            GhFormConOrder.Add(BuildGhFormConItem("1", "isread"));
        }

        protected override void BuildNumberDoctorOrder()
        {
            base.BuildNumberDoctorOrder();

            var timeId = NumberUtil.IntRandom(1, 2);
            DoctorOrder.AddOrUpdate("timeId", timeId);
        }
    }
}
