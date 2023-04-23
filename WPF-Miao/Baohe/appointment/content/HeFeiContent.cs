using Baohe.constants;
using System.Collections.Generic;
using System;
using Utils;
using Baohe.session;
using System.Linq;
using Utils.stringBuilder;

namespace Baohe.appointment.content
{
    internal class HeFeiContent : AppointmentContent
    {
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
            //DoctorOrder.AddOrUpdate(Constant.WaterId, MiaoInfo["NumberSN"]);

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
