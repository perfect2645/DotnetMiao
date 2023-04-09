using Baohe.constants;
using Baohe.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

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
            GhFormConOrder.Add(BuildGhFormConItem("1", "isread"));
        }

        protected override void BuildNumberDoctorOrder()
        {
            base.BuildNumberDoctorOrder();
            DoctorOrder.AddOrUpdate(Constant.WaterId, MiaoInfo["NumberSN"]);

            try
            {
                var numbers = MainSession.MiaoSession["Numbers"] as List<Dictionary<string, object>>;
                if (!numbers.HasItem())
                {
                    return;
                }
                var defaultNumber = numbers.FirstOrDefault();
                var hintType = defaultNumber["HintType"].NotNullString();
                if (string.IsNullOrEmpty(hintType))
                {
                    return;
                }
                if (hintType == "2")
                {
                    DoctorOrder.AddOrUpdate(Constant.WaitingInfor, $"{MiaoInfo["CommendScope"]}");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
