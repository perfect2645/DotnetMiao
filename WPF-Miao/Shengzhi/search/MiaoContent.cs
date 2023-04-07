using Shengzhi.common;
using Shengzhi.login;
using Shengzhi.session;
using System.Collections.Generic;
using Utils;
using Utils.stringBuilder;

namespace Shengzhi.search
{
    internal class MiaoContent : WechatContent
    {

        private static string Url { get; set; }
        private const string BaseUrl = "https://app.quyiyuan.com/APP/appoint/action/AppointActionC.jspx?";
        public Dictionary<string, object> Schedule { get; private set; }
        public MiaoContent(ShengzhiLogin user, Dictionary<string, object> schedule) : base(Url, user)
        {
            Schedule = schedule;
            BuildUrl();
        }

        private void BuildUrl()
        {
            var content = new Dictionary<string, object>();

            content.Add("APPOINT_SOURCE", User.AppointSource);
            content.Add("APP_UUID", User.AppUuid);
            content.Add("CHANNEL_ID", User.ChannelId);

            var dateEncode = UnicodeConverter.Encode(Schedule.GetString("CLINIC_DATE"), true);
            content.Add("CLINIC_DATE", dateEncode);
            content.Add("DEPT_CODE", Schedule.GetString("DEPT_CODE"));
            content.Add("DOCTOR_CODE", Schedule.GetString("DOCTOR_CODE"));
            content.Add("GROUP_CODE", User.GroupCode);
            content.Add("HB_TIME", Schedule.GetString("CLINIC_DURATION"));
            content.Add("IMEI_ID", "");
            content.Add("IS_REFERRAL", "0");
            content.Add("PHONEOPERATINGSYS", "0");
            content.Add("PHONETYPE", "");
            content.Add("PHONEVERSIONNUM", "");
            content.Add("PUBLIC_SERVICE_TYPE", User.PublicServiceType);
            content.Add("hospitalID", MainSession.PlatformSession.GetString(Constants.HospitalId));
            content.Add("loc", "");
            content.Add("op", "getClinicDetailActionC");
            content.Add("opVersion", User.OpVersion);
            content.Add("operateUserSource", User.OperateUserSource);

            var suffix = BuildGetQyCheckSuffix(content);
            content.Add("QY_CHECK_SUFFIX", suffix);

            var urlContent = SbHelper.GetStringContent(content);
            var fullUrl = $"{BaseUrl}{urlContent}";
            RequestUrl = fullUrl;
        }
    }
}
