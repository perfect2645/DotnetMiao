using Baohe.constants;
using Utils;

namespace Baohe.appointment.content
{
    internal class YuhuaKaifaqu1099108 : AppointmentContent
    {
        public YuhuaKaifaqu1099108() : base()
        {

        }
        protected override void BuildDefaultDoctorOrder()
        {
            base.BuildDefaultDoctorOrder();
            DoctorOrder.AddOrUpdate("GH_HosProId", "10");
            DoctorOrder.AddOrUpdate("GH_HosProName", "江苏");
            DoctorOrder.AddOrUpdate("GH_HosCityId", "77");
            DoctorOrder.AddOrUpdate("GH_HosCityName", "南京");
            DoctorOrder.AddOrUpdate("birthday", MemberInfo["Birthday"]);
        }
        protected override void BuildDefaultGhFormCon()
        {
            base.BuildDefaultGhFormCon();
            //GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Familyaddress"], "familyaddress"));
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
