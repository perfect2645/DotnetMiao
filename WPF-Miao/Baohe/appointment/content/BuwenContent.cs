using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.appointment.content
{
    internal class BuwenContent : AppointmentContent
    {
        protected override void BuildDefaultGhFormCon()
        {
            base.BuildDefaultGhFormCon();
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Familyaddress"], "familyaddress"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Cliniccard"], "ClinicCard"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Birthday"], "birthday"));
        }
    }
}
