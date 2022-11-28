namespace Baohe.appointment.content
{
    internal class HeFeiContent : AppointmentContent
    {
        protected override void BuildDefaultGhFormCon()
        {
            base.BuildDefaultGhFormCon();
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Familyaddress"], "familyaddress"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Cliniccard"], "ClinicCard"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Birthday"], "birthday"));
            GhFormConOrder.Add(BuildGhFormConItem("1", "isread"));
        }
    }
}
