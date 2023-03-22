using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Baohe.appointment.content
{
    internal class BuwenContent : AppointmentContent
    {
        protected override void BuildDefaultDoctorOrder()
        {
            base.BuildDefaultDoctorOrder();
            DoctorOrder.AddOrUpdate("GH_HosProId", "13");
            DoctorOrder.AddOrUpdate("GH_HosProName", "福建");
            DoctorOrder.AddOrUpdate("GH_HosCityId", "123");
            DoctorOrder.AddOrUpdate("GH_HosCityName", "漳州");
        }

        protected override void BuildDefaultGhFormCon()
        {
            base.BuildDefaultGhFormCon();
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Familyaddress"], "familyaddress"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Cliniccard"], "ClinicCard"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Birthday"], "birthday"));
        }
    }
}
