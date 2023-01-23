using Shengzhi.common;
using Shengzhi.login;
using Shengzhi.session;
using Utils;

namespace Shengzhi.appointment
{
    internal class YuyueContent : WechatContent
    {
        private static string url = "https://app.quyiyuan.com/APP/appoint/action/AppointActionC.jspx";
        public Order Order { get; private set; }
        public YuyueContent(Order order, ShengzhiLogin user) : base(url, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("DEPT_CODE", MainSession.PlatformSession.GetString(Constants.DeptId));
            AddContent("DEPT_NAME", MainSession.PlatformSession.GetString(Constants.DeptName));
            AddContent("DOCTOR_CODE", MainSession.PlatformSession.GetString(Constants.DoctorId));
            AddContent("DOCTOR_NAME", MainSession.PlatformSession.GetString(Constants.DoctorName));
            AddContent("MARK_DESC", MainSession.PlatformSession.GetString(Constants.DoctorName));
            AddContent("CLINIC_TYPE", "免费号");
            AddContent("REG_DATE", Order.RegDate);
            AddContent("CLINIC_DURATION", Order.ClinicDuration);
            AddContent("APPT_MADE_DATE", Order.AppMadeDate);
            AddContent("AMOUNT_TEXT", Order.AmountText);
            AddContent("AMOUNT", Order.Amount);
            AddContent("AMOUNT_TYPE", "0");
            AddContent("PATIENT_ID", User.PatientId);
            AddContent("PATIENT_NAME", User.UserName);
            AddContent("ID_NO", "");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");
            AddContent("PATIENT_ID", "0");




            AddContent("APPOINT_SOURCE", Order.AppointScore);
            AddContent("APP_UUID", Order.AppUuid);
            AddContent("CHANNEL_ID", Order.ChannelId);
            AddContent("GROUP_CODE", Order.GroupCode);
            AddContent("HOSPITAL_WX_OPEN_ID", Order.HospitalWxOpenId);
            AddContent("IMEI_ID", Order.ImeiId);
            AddContent("PHONEOPERATINGSYS", Order.PhoneOperationSys);
            AddContent("PHONETYPE", Order.PhoneType);
            AddContent("PHONEVERSIONNUM", Order.PhoneVersionNum);
            AddContent("PUBLIC_SERVICE_TYPE", Order.PublicServiceType);
            AddContent("QY_CHECK_SUFFIX", Order.QyCheckSuffix);
            AddContent("USER_ID", Order.UserId);
            AddContent("USER_VS_ID", Order.UserVsId);
            AddContent("hospitalID", Order.HospitalId);
            AddContent("isLogin", true);
            AddContent("loc", "c");
            AddContent("op", "monitorRecords");
            AddContent("opVersion", Order.OpVersion);
            AddContent("operateCurrent_UserId", Order.UserId);
            AddContent("operateUserSource", Order.OperateUserSource);
        }

        //private void BuildContent()
        //{
        //    AddContent("APPOINT_SOURCE", Order.AppointScore);
        //    AddContent("APP_UUID", Order.AppUuid);
        //    AddContent("CHANNEL_ID", Order.ChannelId);
        //    AddContent("GROUP_CODE", Order.GroupCode);
        //    AddContent("HOSPITAL_WX_OPEN_ID", Order.HospitalWxOpenId);
        //    AddContent("IMEI_ID", Order.ImeiId);
        //    AddContent("PHONEOPERATINGSYS", Order.PhoneOperationSys);
        //    AddContent("PHONETYPE", Order.PhoneType);
        //    AddContent("PHONEVERSIONNUM", Order.PhoneVersionNum);
        //    AddContent("PUBLIC_SERVICE_TYPE", Order.PublicServiceType);
        //    AddContent("QY_CHECK_SUFFIX", Order.QyCheckSuffix);
        //    AddContent("USER_ID", Order.UserId);
        //    AddContent("USER_VS_ID", Order.UserVsId);
        //    AddContent("hospitalID", Order.HospitalId);
        //    AddContent("isLogin", true);
        //    AddContent("loc", "c");
        //    AddContent("op", "monitorRecords");
        //    AddContent("opVersion", Order.OpVersion);
        //    AddContent("operateCurrent_UserId", Order.UserId);
        //    AddContent("operateUserSource", Order.OperateUserSource);
        //}
    }
}
