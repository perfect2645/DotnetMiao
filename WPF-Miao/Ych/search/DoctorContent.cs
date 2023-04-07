using Utils;
using Utils.datetime;
using Ych.common;
using Ych.session;

namespace Ych.search
{
    internal class DoctorContent : YchContent
    {
        private const string url = "/wechatclient/api/auth/appointment/listDoctor";

        public string Date { get; set; }

        public DoctorContent(string date) : base(url)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("departmentCode", MainSession.PlatformSession.GetString(Constants.DeptId));
            AddContent("scheduleDate", Date);
            AddContent("outpatientTpye", "1");

            var timestamp = DateTimeUtil.GetTimeStamp();
            AddContent("timestamp", timestamp);

            AddContent("sign", GetContentsMD5());
        }
    }
}
