using Shengzhi.common;
using Shengzhi.login;
using System.Collections.Generic;
using Utils.stringBuilder;

namespace Shengzhi.search
{
    internal class MiaoContent : WechatContent
    {

        private static string Url { get; set; }
        private const string BaseUrl = "https://app.quyiyuan.com/APP/appoint/action/AppointActionC.jspx?";
        public MiaoContent(ShengzhiLogin user) : base(Url, user)
        {
        }

        private void BuildUrl()
        {
            var content = new Dictionary<string, object>();
            content.Add("APPOINT_SOURCE", "");
            content.Add("APP_UUID", "");
            content.Add("CHANNEL_ID", "");
            content.Add("CLINIC_DATE", "");
            content.Add("DEPT_CODE", "");
            content.Add("DOCTOR_CODE", "");
            content.Add("GROUP_CODE", "");
            content.Add("HB_TIME", "");
            content.Add("HOSPITAL_WX_OPEN_ID", "");
            content.Add("IMEI_ID", "");
            content.Add("IS_REFERRAL", "");
            content.Add("PHONEOPERATINGSYS", "");
            content.Add("PHONETYPE", "");
            content.Add("PHONEVERSIONNUM", "");
            content.Add("PUBLIC_SERVICE_TYPE", "");
            content.Add("USER_ID", "");
            content.Add("hospitalID", "");
            content.Add("isLogin", "");
            content.Add("loc", "");
            content.Add("op", "");
            content.Add("opVersion", "");
            content.Add("operateCurrent_UserId", "");
            content.Add("operateUserSource", "");
            content.Add("QY_CHECK_SUFFIX", "");


            var urlContent = SbHelper.GetStringContent(content);
            var fullUrl = $"{BaseUrl}{urlContent}";
            RequestUrl = fullUrl;
        }
    }
}
