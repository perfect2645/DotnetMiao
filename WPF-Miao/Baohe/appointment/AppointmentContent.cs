using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Utils.stringBuilder;

namespace Baohe.appointment
{
    internal class AppointmentContent : ContentBase
    {

        public Dictionary<string, object> MemberInfo { get; set; }

        public Dictionary<string, object> MiaoInfo { get; set; }

        private Dictionary<string, object> DoctorOrder { get; set; }
        private Dictionary<string, object> GhFormConOrder { get; set; }

        public AppointmentContent() : base("https://appoint.yihu.com/appoint/do/registerInfo/register")
        {
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

            AddContent("DoctorOrder", DoctorOrder);
            AddContent("ghFormCon", GhFormConOrder);
        }

        private void BuildDefaultDoctorOrder()
        {
            DoctorOrder = new Dictionary<string, object>();

            var platformSesstion = BaoheSession.PlatformSesstion;
            var doctorInfo = SessionBuilder.GetDefaultDoctor();

            DoctorOrder.Add("memberSn", MemberInfo["Membersn"]);
            DoctorOrder.Add("memberName", MemberInfo["Cname"]);
            DoctorOrder.Add("memberPhone", MemberInfo["Phone"]);
            DoctorOrder.Add("memberAddress", MemberInfo["Familyaddress"]);
            DoctorOrder.Add("memberBirthday", MemberInfo["Birthday"]);
            DoctorOrder.Add("memberSex", MemberInfo["Sex"]);
            DoctorOrder.Add("memberId", MemberInfo["Memberid"]);
            DoctorOrder.Add("othercard", MemberInfo["Othercard"]);
            DoctorOrder.Add("guardianmembersn", MemberInfo["Guardianmembersn"]);
            DoctorOrder.Add("identitytype", MemberInfo["Identitytype"]);
            DoctorOrder.Add("memberZhengjianid", MemberInfo["Zhengjianid"]);
            DoctorOrder.Add("memberIdcard", MemberInfo["Idcard"]);
            DoctorOrder.Add("accManageGoodSn", null);
            DoctorOrder.Add("regpaytype", "");
            DoctorOrder.Add("cliniccard", MemberInfo["Cliniccard"]);
            DoctorOrder.Add("applyNo", "");
            DoctorOrder.Add("mobile", MemberInfo["Phone"]);

            DoctorOrder.Add("hosDeptId", platformSesstion[Constant.DeptId]);

            DoctorOrder.Add("doctorService_gh", doctorInfo["doctorService_gh"]);
            DoctorOrder.Add("doctorUid", doctorInfo["doctorUid"]);
            DoctorOrder.Add("doctorName", doctorInfo["doctorName"]);
            DoctorOrder.Add("doctorPic", "");
            DoctorOrder.Add("doctorClinicName", doctorInfo["lczcName"]);
            DoctorOrder.Add("GH_HosProId", "12");
            DoctorOrder.Add("GH_HosProName", "安徽");
            DoctorOrder.Add("GH_HosCityId", "101");
            DoctorOrder.Add("GH_HosCityName", "合肥");

            DoctorOrder.Add("timeId", 1);

            DoctorOrder.Add("ghAmount", 0);
            DoctorOrder.Add("securityDeposit", 0);
            DoctorOrder.Add("ghfeeway", 0);
            DoctorOrder.Add("ModeId", 0);
            DoctorOrder.Add("GhFee", 0);
            DoctorOrder.Add("AllFee", 0);

            DoctorOrder.Add("UnOpened", false);

            DoctorOrder.Add(Constant.AccountSn, sessionDic[Constant.AccountSn]);
            DoctorOrder.Add("cardNumber", sessionDic["cardNumber"]);
            DoctorOrder.Add(Constant.LoginId, sessionDic[Constant.LoginId]);
            DoctorOrder.Add(Constant.ChannelId, platformSesstion[Constant.LoginChannel]);
            DoctorOrder.Add("utm_source", platformSesstion["jkzlAn_utm_source"]);//.0.h.1026.bus010.0
            DoctorOrder.Add("doctorOfficeName", "");
        }

        private void BuildDefaultGhFormCon()
        {
        }

        private void BuildNumberDoctorOrder()
        {
            DoctorOrder.Add("registerDate", MiaoInfo["registerdate"]);
            DoctorOrder.Add("hosDeptName", MiaoInfo["deptname"]);
            DoctorOrder.Add("hospitalId", MiaoInfo["hosid"]);
            DoctorOrder.Add("hospitalName", MiaoInfo["hosname"]);
            DoctorOrder.Add("availablenum", MiaoInfo["availablenum"].ToString()!.ToLong());
            DoctorOrder.Add("FHTimes", MiaoInfo["FHTimes"]);
            DoctorOrder.Add("FHDays", MiaoInfo["FHDays"]);

            DoctorOrder.Add(Constant.WaterId, MiaoInfo["NumberSN"].ToString()!.ToLong());
            DoctorOrder.Add(Constant.WaitingInfor, $"第{MiaoInfo["SerialNo"]}号 {MiaoInfo["CommendScope"]}");
            DoctorOrder.Add("store", "");
            DoctorOrder.Add("serialNo", MiaoInfo["SerialNo"]);
            DoctorOrder.Add(Constant.DoctorSn, MiaoInfo["DoctorSN"]);
            DoctorOrder.Add("arrangeId", MiaoInfo["ArrangeID"].ToString()!.ToLong());
        }

        private void BuildNumberGhFormCon()
        {

        }

        private Dictionary<string, object> BuildDoctorOrder()
        {
            var sessionDic = Session.SessionDic;
            var platformSesstion = BaoheSession.PlatformSesstion;
            var arrangeWater = SessionBuilder.GetAvailableArrangeWater();
            var defaultNumber = SessionBuilder.GetDefaultNumber(Session);
            var member = SessionBuilder.GetDefaultMember(Session);
            var doctorInfo = SessionBuilder.GetDefaultDoctor();


            var DoctorOrder = new Dictionary<string, object>();
            DoctorOrder.Add(Constant.WaterId, defaultNumber["NumberSN"].ToString()!.ToLong());
            DoctorOrder.Add(Constant.WaitingInfor, $"第{defaultNumber["SerialNo"]}号 {defaultNumber["CommendScope"]}");
            DoctorOrder.Add("store", "");
            DoctorOrder.Add("serialNo", defaultNumber["SerialNo"]);
            DoctorOrder.Add("memberSn", member["Membersn"]);
            DoctorOrder.Add("memberName", member["Cname"]);
            DoctorOrder.Add("memberPhone", member["Phone"]);
            DoctorOrder.Add("memberAddress", member["Familyaddress"]);
            DoctorOrder.Add("memberBirthday", member["Birthday"]);
            DoctorOrder.Add("memberSex", member["Sex"]);
            DoctorOrder.Add("memberId", member["Memberid"]);
            DoctorOrder.Add("othercard", member["Othercard"]);
            DoctorOrder.Add("guardianmembersn", member["Guardianmembersn"]);
            DoctorOrder.Add("identitytype", member["Identitytype"]);
            DoctorOrder.Add("memberZhengjianid", member["Zhengjianid"]);
            DoctorOrder.Add("memberIdcard", member["Idcard"]);
            DoctorOrder.Add("accManageGoodSn", null);
            DoctorOrder.Add("regpaytype", "");
            DoctorOrder.Add("cliniccard", member["Cliniccard"]);
            DoctorOrder.Add("applyNo", "");
            DoctorOrder.Add("mobile", member["Phone"]);
            DoctorOrder.Add(Constant.DoctorSn, defaultNumber["DoctorSN"]);
            DoctorOrder.Add("hosDeptId", platformSesstion[Constant.DeptId]);
            DoctorOrder.Add("hosDeptName", arrangeWater["deptname"]);
            DoctorOrder.Add("hospitalId", arrangeWater["hosid"]);
            DoctorOrder.Add("hospitalName", arrangeWater["hosname"]);
            DoctorOrder.Add("doctorService_gh", doctorInfo["doctorService_gh"]);
            DoctorOrder.Add("doctorUid", doctorInfo["doctorUid"]);
            DoctorOrder.Add("doctorName", doctorInfo["doctorName"]);
            DoctorOrder.Add("doctorPic", "");
            DoctorOrder.Add("doctorClinicName", doctorInfo["lczcName"]);
            DoctorOrder.Add("GH_HosProId", "12");
            DoctorOrder.Add("GH_HosProName", "安徽");
            DoctorOrder.Add("GH_HosCityId", "101");
            DoctorOrder.Add("GH_HosCityName", "合肥");
            DoctorOrder.Add("registerDate", arrangeWater["registerdate"]);
            DoctorOrder.Add("timeId", 1);
            DoctorOrder.Add("arrangeId", defaultNumber["ArrangeID"].ToString()!.ToLong());
            DoctorOrder.Add("ghAmount", 0);
            DoctorOrder.Add("securityDeposit", 0);
            DoctorOrder.Add("ghfeeway", 0);
            DoctorOrder.Add("ModeId", 0);
            DoctorOrder.Add("GhFee", 0);
            DoctorOrder.Add("AllFee", 0);
            DoctorOrder.Add("availablenum", arrangeWater["availablenum"].ToString()!.ToLong());
            DoctorOrder.Add("UnOpened", false);
            DoctorOrder.Add("FHTimes", arrangeWater["FHTimes"]);
            DoctorOrder.Add("FHDays", arrangeWater["FHDays"]);
            DoctorOrder.Add(Constant.AccountSn, sessionDic[Constant.AccountSn]);
            DoctorOrder.Add("cardNumber", sessionDic["cardNumber"]);
            DoctorOrder.Add(Constant.LoginId, sessionDic[Constant.LoginId]);
            DoctorOrder.Add(Constant.ChannelId, platformSesstion[Constant.LoginChannel]);
            DoctorOrder.Add("utm_source", platformSesstion["jkzlAn_utm_source"]);//.0.h.1026.bus010.0
            DoctorOrder.Add("doctorOfficeName", "");
            //DoctorOrder.Add("retId", "8f1adb4a37e143a885d53db93f803eeb");
            //DoctorOrder.Add("graphAuthCode", sessionDic["graphAuthCode"]);
            

            return DoctorOrder;
        }

        private List<Dictionary<string, object>> BuildGhFormCon()
        {
            var sessionDic = Session.SessionDic;
            var platformSesstion = BaoheSession.PlatformSesstion;
            var arrangeWater = SessionBuilder.GetAvailableArrangeWater();
            var defaultNumber = SessionBuilder.GetDefaultNumber(Session);
            var member = SessionBuilder.GetDefaultMember(Session);
            var doctorInfo = SessionBuilder.GetDefaultDoctor();

            var ghFormCon = new List<Dictionary<string, object>>();

            ghFormCon.Add(BuildGhFormConItem(member["Familyaddress"], "familyaddress"));
            //ghFormCon.Add(BuildGhFormConItem(member["Cname"], "name"));
            ghFormCon.Add(BuildGhFormConItem("", "name"));
            ghFormCon.Add(BuildGhFormConItem(member["Cliniccard"], "ClinicCard"));
            ghFormCon.Add(BuildGhFormConItem(member["Idcard"], "CardNo"));
            ghFormCon.Add(BuildGhFormConItem(member["Sex"], "sex"));
            ghFormCon.Add(BuildGhFormConItem(member["Phone"], "mobile"));
            ghFormCon.Add(BuildGhFormConItem(member["Identitytype"], "cardtype"));
            ghFormCon.Add(BuildGhFormConItem("0", "cmb_disease"));
            ghFormCon.Add(BuildGhFormConItem("0", "cmb_disease"));
            ghFormCon.Add(BuildGhFormConItem("未确诊", "cmb_diseaseName"));

            return ghFormCon;
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
