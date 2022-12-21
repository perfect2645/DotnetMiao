using Utils.datetime;
using Ych.common;
using Ych.session;

namespace Ych.search
{
    internal class DoctorContent : YchContent
    {
        private const string url = "http://www.szychrmyy.com/wechatclient/api/auth/appointment/listNumber";

        public DoctorContent() : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.DeptId);
            AddContent(MainSession.PlatformSession, Constants.DoctorId);
            AddContent(MainSession.PlatformSession, Constants.DoctorType);
            AddContent("outpatientTpye", "1");

            var timestamp = DateTimeUtil.GetTimeStamp();
            AddContent("timestamp", timestamp);

            AddContent("sign", GetContentsMD5());
        }
    }
}
