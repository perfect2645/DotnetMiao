using Shengzhi.common;
using Shengzhi.login;
using Shengzhi.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var depId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);

            content.Add("APPOINT_SOURCE", User.AppointSource);
            content.Add("APP_UUID", User.AppUuid);
            content.Add("BUSSINESS_TYPE", "2");
            content.Add("CHANNEL_ID", User.ChannelId);

            content.Add("DEPT_CODE", depId);
            content.Add("DOCTOR_CODE", doctorId);
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
