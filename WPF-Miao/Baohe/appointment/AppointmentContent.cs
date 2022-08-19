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
        private readonly ISessionItem Session;

        public AppointmentContent(string url, ISessionItem session) : base(url)
        {
            Session = session;
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            base.BuildDefaultHeaders(httpClient);
        }

        private void BuildContent()
        {
            var doctorOrder = BuildDoctorOrder();
            var ghFormCon = BuildGhFormCon();
            AddContent("doctorRegOrder", doctorOrder);
            AddContent("ghFormCon", ghFormCon);
        }

        #region Content

        private Dictionary<string, object> BuildDoctorOrder()
        {
            var sessionDic = Session.SessionDic;
            var platformSesstion = BaoheSession.PlatformSesstion;
            var arrangeWater = SessionBuilder.GetMaxArrangeWater(Session);
            var defaultNumber = SessionBuilder.GetDefaultNumber(Session);
            var member = SessionBuilder.GetDefaultMember(Session);
            var doctorInfo = SessionBuilder.GetDefaultDoctor();


            var doctorRegOrder = new Dictionary<string, object>();
            doctorRegOrder.Add(Constant.WaterId, defaultNumber["NumberSN"].ToString()!.ToLong());
            doctorRegOrder.Add(Constant.WaitingInfor, $"第{defaultNumber["SerialNo"]}号 {defaultNumber["CommendScope"]}");
            doctorRegOrder.Add("store", "");
            doctorRegOrder.Add("serialNo", defaultNumber["SerialNo"]);
            doctorRegOrder.Add("memberSn", member["Membersn"]);
            doctorRegOrder.Add("memberName", member["Cname"]);
            doctorRegOrder.Add("memberPhone", member["Phone"]);
            doctorRegOrder.Add("memberAddress", member["Familyaddress"]);
            doctorRegOrder.Add("memberBirthday", member["Birthday"]);
            doctorRegOrder.Add("memberSex", member["Sex"]);
            doctorRegOrder.Add("memberId", member["Memberid"]);
            doctorRegOrder.Add("othercard", member["Othercard"]);
            doctorRegOrder.Add("guardianmembersn", member["Guardianmembersn"]);
            doctorRegOrder.Add("identitytype", member["Identitytype"]);
            doctorRegOrder.Add("memberZhengjianid", member["Zhengjianid"]);
            doctorRegOrder.Add("memberIdcard", member["Idcard"]);
            doctorRegOrder.Add("accManageGoodSn", null);
            doctorRegOrder.Add("regpaytype", "");
            doctorRegOrder.Add("cliniccard", member["Cliniccard"]);
            doctorRegOrder.Add("applyNo", "");
            doctorRegOrder.Add("mobile", member["Phone"]);
            doctorRegOrder.Add(Constant.DoctorSn, defaultNumber["DoctorSN"]);
            doctorRegOrder.Add("hosDeptId", platformSesstion[Constant.DeptId]);
            doctorRegOrder.Add("hosDeptName", arrangeWater["deptname"]);
            doctorRegOrder.Add("hospitalId", arrangeWater["hosid"]);
            doctorRegOrder.Add("hospitalName", arrangeWater["hosname"]);
            doctorRegOrder.Add("doctorService_gh", doctorInfo["doctorService_gh"]);
            doctorRegOrder.Add("doctorUid", doctorInfo["doctorUid"]);
            doctorRegOrder.Add("doctorName", doctorInfo["doctorName"]);
            doctorRegOrder.Add("doctorPic", "");
            doctorRegOrder.Add("doctorClinicName", doctorInfo["lczcName"]);
            doctorRegOrder.Add("GH_HosProId", "12");
            doctorRegOrder.Add("GH_HosProName", "安徽");
            doctorRegOrder.Add("GH_HosCityId", "101");
            doctorRegOrder.Add("GH_HosCityName", "合肥");
            doctorRegOrder.Add("registerDate", arrangeWater["registerdate"]);
            doctorRegOrder.Add("timeId", 1);
            doctorRegOrder.Add("arrangeId", defaultNumber["ArrangeID"].ToString()!.ToLong());
            doctorRegOrder.Add("ghAmount", 0);
            doctorRegOrder.Add("securityDeposit", 0);
            doctorRegOrder.Add("ghfeeway", 0);
            doctorRegOrder.Add("ModeId", 0);
            doctorRegOrder.Add("GhFee", 0);
            doctorRegOrder.Add("AllFee", 0);
            doctorRegOrder.Add("availablenum", arrangeWater["availablenum"].ToString()!.ToLong());
            doctorRegOrder.Add("UnOpened", false);
            doctorRegOrder.Add("FHTimes", arrangeWater["FHTimes"]);
            doctorRegOrder.Add("FHDays", arrangeWater["FHDays"]);
            doctorRegOrder.Add(Constant.AccountSn, sessionDic[Constant.AccountSn]);
            doctorRegOrder.Add("cardNumber", sessionDic["cardNumber"]);
            doctorRegOrder.Add(Constant.LoginId, sessionDic[Constant.LoginId]);
            doctorRegOrder.Add(Constant.ChannelId, platformSesstion[Constant.LoginChannel]);
            doctorRegOrder.Add("utm_source", platformSesstion["jkzlAn_utm_source"]);//.0.h.1026.bus010.0
            doctorRegOrder.Add("doctorOfficeName", "");
            //doctorRegOrder.Add("retId", "8f1adb4a37e143a885d53db93f803eeb");

            return doctorRegOrder;
        }

        private List<Dictionary<string, object>> BuildGhFormCon()
        {
            var sessionDic = Session.SessionDic;
            var platformSesstion = BaoheSession.PlatformSesstion;
            var arrangeWater = SessionBuilder.GetMaxArrangeWater(Session);
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
