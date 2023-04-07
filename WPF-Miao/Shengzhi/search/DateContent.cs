using Shengzhi.common;
using Shengzhi.login;
using Shengzhi.session;
using System.Collections.Generic;
using System.Text;
using Utils;
using Utils.stringBuilder;

namespace Shengzhi.search
{
    internal class DateContent : WechatContent
    {
        private static string Url { get; set; }
        private const string BaseUrl = "https://app.quyiyuan.com/APP/appoint/action/AppointActionC.jspx?";
        public DateContent(ShengzhiLogin user) : base(Url, user)
        {
            BuildUrl();
        }

        private void BuildUrl()
        {
            var content = new Dictionary<string, object>();

            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var depId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);

            content.Add("APPOINT_SOURCE", User.AppointSource);
            content.Add("APP_UUID", User.AppUuid);
            content.Add("BUSSINESS_TYPE", "2");
            content.Add("CHANNEL_ID", User.ChannelId);
            content.Add("DEPT_CODE", depId);
            content.Add("GROUP_CODE", User.GroupCode);
            content.Add("HOSPITAL_WX_OPEN_ID", User.HospitalWxOpenId);
            content.Add("IMEI_ID", User.ImeiId);
            content.Add("IS_ONLINE", "0");
            content.Add("IS_REFERRAL", "0");
            content.Add("PHONEOPERATINGSYS", User.PhoneOperationSys);
            content.Add("PHONETYPE", User.PhoneType);
            content.Add("PHONEVERSIONNUM", User.PhoneVersionNum);
            content.Add("PUBLIC_SERVICE_TYPE", User.PublicServiceType);
            content.Add("USER_ID", User.LoginInfo.GetString("USER_ID"));
            content.Add("USER_VS_ID", User.UserVsId);
            content.Add("appointId", "");
            content.Add("hospitalID", hospitalId);
            content.Add("isLogin", "true");
            content.Add("loc", User.Loc);
            content.Add("op", "getDoctorListActionC");
            content.Add("opVersion", User.OpVersion);
            content.Add("operateCurrent_UserId", User.UserId);
            content.Add("operateUserSource", "0");
            content.Add("realFlag", "");

            var suffix = BuildGetQyCheckSuffix(content);
            content.Add("QY_CHECK_SUFFIX", suffix);


            var urlContent = SbHelper.GetStringContent(content);
            var fullUrl = $"{BaseUrl}{urlContent}";
            RequestUrl = fullUrl;
        }
    }
}
