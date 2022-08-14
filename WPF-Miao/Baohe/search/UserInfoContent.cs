using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;

namespace Baohe.search
{
    internal class UserInfoContent : ContentBase
    {
        public UserInfoContent(string url) : base(url)
        {
        }

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];
            var time = BaoheSession.PlatformSesstion[Constant.SessionTime];

            var refererTemplate = $"https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

            return refererTemplate;
        }
    }
}
