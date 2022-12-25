using Utils;
using Utils.datetime;
using Zhi.common;
using Zhi.session;

namespace Zhi.search
{
    internal class DoctorContent : ZhiContent
    {
        private const string url = "http://www.szZhirmyy.com/wechatclient/api/auth/appointment/listDoctor";

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
