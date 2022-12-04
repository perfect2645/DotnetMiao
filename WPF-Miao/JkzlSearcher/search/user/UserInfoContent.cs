using JkzlSearcher.common;
using JkzlSearcher.session;
using Utils.datetime;

namespace JkzlSearcher.search.user
{
    internal class UserInfoContent : JkzlContent
    {
        public UserInfoContent(string url) : base(url)
        {
        }

        public string BuildReferer()
        {
            var platformType = MainSession.PlatformSession[Constants.PlatformType];
            var hospitalId = MainSession.PlatformSession[Constants.HospitalId];
            var time =DateTimeUtil.GetTimeStamp();

            var refererTemplate = $"https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

            return refererTemplate;
        }
    }
}
