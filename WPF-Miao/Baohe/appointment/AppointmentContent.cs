using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Utils;
using Utils.stringBuilder;

namespace Baohe.appointment
{
    internal class AppointmentContent : ContentBase
    {
        public Dictionary<string, object> MemberInfo { get; set; }

        public Dictionary<string, object> MiaoInfo { get; set; }

        private Dictionary<string, object> DoctorOrder { get; set; }
        private List<Dictionary<string, object>> GhFormConOrder { get; set; }

        public AppointmentContent(Dictionary<string, object> memberInfo) : base("https://appoint.yihu.com/appoint/do/registerInfo/register")
        {
            MemberInfo = memberInfo;
            ContentType = "application/x-www-form-urlencoded";
            InitContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            base.BuildDefaultHeaders(httpClient);
        }

        #region Content

        private void InitContent()
        {
            BuildDefaultDoctorOrder();
            BuildDefaultGhFormCon();

        }

        public void FillContent()
        {
            BuildNumberDoctorOrder();
            BuildNumberGhFormCon();

            AddContent("doctorRegOrder", DoctorOrder);
            AddContent("ghFormCon", GhFormConOrder);
        }

        private void BuildDefaultDoctorOrder()
        {
            DoctorOrder = new Dictionary<string, object>();

            var platformSesstion = BaoheSession.PlatformSesstion;
            var doctorInfo = SessionBuilder.GetDefaultDoctor();

            DoctorOrder.AddOrUpdate("memberSn", MemberInfo["Membersn"]);
            DoctorOrder.AddOrUpdate("memberName", MemberInfo["Cname"]);
            DoctorOrder.AddOrUpdate("memberPhone", MemberInfo[Constant.Phone]);
            DoctorOrder.AddOrUpdate("memberAddress", MemberInfo["Familyaddress"]);
            DoctorOrder.AddOrUpdate("memberBirthday", MemberInfo["Birthday"]);
            DoctorOrder.AddOrUpdate("memberSex", MemberInfo["Sex"]);
            DoctorOrder.AddOrUpdate("memberId", MemberInfo["Memberid"]);
            DoctorOrder.AddOrUpdate("othercard", MemberInfo["Othercard"]);
            DoctorOrder.AddOrUpdate("guardianmembersn", MemberInfo["Guardianmembersn"]);
            DoctorOrder.AddOrUpdate("identitytype", MemberInfo["Identitytype"]);
            DoctorOrder.AddOrUpdate("memberZhengjianid", MemberInfo["Zhengjianid"]);
            DoctorOrder.AddOrUpdate("memberIdcard", MemberInfo["Idcard"]);
            DoctorOrder.AddOrUpdate("accManageGoodSn", null);
            DoctorOrder.AddOrUpdate("regpaytype", "");
            DoctorOrder.AddOrUpdate("cliniccard", MemberInfo["Cliniccard"]);
            DoctorOrder.AddOrUpdate("applyNo", "");
            DoctorOrder.AddOrUpdate("mobile", MemberInfo["Phone"]);
            DoctorOrder.AddOrUpdate(Constant.accountSn, MemberInfo[Constant.Accountsn]);
            DoctorOrder.AddOrUpdate("cardNumber", BaoheSession.UserSession["cardNumber"]);

            DoctorOrder.AddOrUpdate("hosDeptId", platformSesstion[Constant.DeptId]);

            DoctorOrder.AddOrUpdate("doctorService_gh", doctorInfo["doctorService_gh"]);
            DoctorOrder.AddOrUpdate("doctorUid", doctorInfo["doctorUid"]);
            DoctorOrder.AddOrUpdate("doctorName", doctorInfo["doctorName"]);
            DoctorOrder.AddOrUpdate("doctorPic", "");
            DoctorOrder.AddOrUpdate("doctorClinicName", doctorInfo["lczcName"]);
            DoctorOrder.AddOrUpdate("GH_HosProId", "12");
            DoctorOrder.AddOrUpdate("GH_HosProName", "安徽");
            DoctorOrder.AddOrUpdate("GH_HosCityId", "101");
            DoctorOrder.AddOrUpdate("GH_HosCityName", "合肥");

            DoctorOrder.AddOrUpdate("timeId", 1);

            DoctorOrder.AddOrUpdate("ghAmount", 0);
            DoctorOrder.AddOrUpdate("securityDeposit", 0);
            DoctorOrder.AddOrUpdate("ghfeeway", 0);
            DoctorOrder.AddOrUpdate("ModeId", 0);
            DoctorOrder.AddOrUpdate("GhFee", 0);
            DoctorOrder.AddOrUpdate("AllFee", 0);

            DoctorOrder.AddOrUpdate("UnOpened", false);


            DoctorOrder.AddOrUpdate(Constant.LoginId, platformSesstion[Constant.Loginid]);
            DoctorOrder.AddOrUpdate(Constant.ChannelId, platformSesstion[Constant.PlatformType]);
            //DoctorOrder.AddOrUpdate("utm_source", platformSesstion["jkzlAn_utm_source"]);//.0.h.1026.bus010.0
            DoctorOrder.AddOrUpdate("doctorOfficeName", "");
            DoctorOrder.AddOrUpdate("isread", "1");

            if (platformSesstion.ContainsKey(Constant.RetId))
            {
                DoctorOrder.AddOrUpdate("retid", platformSesstion[Constant.RetId]);
            }
        }

        private void BuildDefaultGhFormCon()
        {
            GhFormConOrder = new List<Dictionary<string, object>>();
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Familyaddress"], "familyaddress"));
            //ghFormCon.AddOrUpdate(BuildGhFormConItem(member["Cname"], "name"));
            GhFormConOrder.Add(BuildGhFormConItem("", "name"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Cliniccard"], "ClinicCard"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Idcard"], "CardNo"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Birthday"], "birthday"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Sex"], "sex"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Phone"], "mobile"));
            GhFormConOrder.Add(BuildGhFormConItem(MemberInfo["Identitytype"], "cardtype"));
            GhFormConOrder.Add(BuildGhFormConItem("0", "cmb_disease"));
            GhFormConOrder.Add(BuildGhFormConItem("0", "cmb_disease"));
            GhFormConOrder.Add(BuildGhFormConItem("未确诊", "cmb_diseaseName"));
            GhFormConOrder.Add(BuildGhFormConItem("1", "isread"));
        }

        private void BuildNumberDoctorOrder()
        {
            var water = MiaoInfo["arrangeWater"].Dic();
            DoctorOrder.AddOrUpdate("registerDate", water["registerdate"]);
            DoctorOrder.AddOrUpdate("hosDeptName", water["deptname"]);
            DoctorOrder.AddOrUpdate("hospitalId", water["hosid"]);
            DoctorOrder.AddOrUpdate("hospitalName", water["hosname"]);
            DoctorOrder.AddOrUpdate("availablenum", water["availablenum"].ToString()!.ToLong());
            DoctorOrder.AddOrUpdate("FHTimes", water["FHTimes"]);
            DoctorOrder.AddOrUpdate("FHDays", water["FHDays"]);

            DoctorOrder.AddOrUpdate(Constant.WaterId, MiaoInfo["NumberSN"].ToString()!.ToLong());
            DoctorOrder.AddOrUpdate(Constant.WaitingInfor, $"第{MiaoInfo["SerialNo"]}号 {MiaoInfo["CommendScope"]}");
            DoctorOrder.AddOrUpdate("store", "");
            DoctorOrder.AddOrUpdate("serialNo", MiaoInfo["SerialNo"]);
            DoctorOrder.AddOrUpdate(Constant.DoctorSn, MiaoInfo["DoctorSN"]);
            DoctorOrder.AddOrUpdate("arrangeId", MiaoInfo["ArrangeID"].ToString()!.ToLong());
        }

        private void BuildNumberGhFormCon()
        {

        }

        private Dictionary<string, object> BuildGhFormConItem(object keyValue, string keyName)
        {
            return new Dictionary<string, object>
            {
                {"keyValue", keyValue },
                {"keyName", keyName },
            };
        }



        #endregion Content

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];
            var time = BaoheSession.PlatformSesstion[Constant.SessionTime];

            var refererTemplate = $"https://appoint.yihu.com/appoint/register/registerOrder.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

            return refererTemplate;
        }
    }
}
