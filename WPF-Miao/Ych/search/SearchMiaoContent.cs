using Utils;
using Utils.datetime;
using Ych.common;
using Ych.session;

namespace Ych.search
{
    internal class SearchMiaoContent : YchContent
    {
        private const string url = "/wechatclient/api/auth/appointment/listNumber";

        public string TimeFlag { get; private set; }
        public string Date { get; private set; }
        public string DoctorId { get; private set; }
        public string DoctorType { get; private set; }
        public SearchMiaoContent(string date, string timeFlag, string doctorId, string doctorType) : base(url)
        {
            Date = date;
            TimeFlag = timeFlag;
            DoctorId = doctorId;
            DoctorType = doctorType;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("departmentCode", MainSession.PlatformSession.GetString(Constants.DeptId));
            AddContent("doctorCode", DoctorId);
            AddContent("schedulingDate", Date);
            AddContent(Constants.DoctorType, DoctorType);
            AddContent(Constants.TimeFlag, TimeFlag);
            AddContent("outpatientTpye", "1");

            var timestamp = DateTimeUtil.GetTimeStamp();
            AddContent("timestamp", timestamp);

            AddContent("sign", GetContentsMD5());
        }
    }
}
