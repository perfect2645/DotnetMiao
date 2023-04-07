using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;

namespace Baohe.search.user
{
    internal class UserInfoContent : ContentBase
    {
        public UserInfoContent(string url) : base(url)
        {
        }

        public string BuildReferer()
        {
            var platformType = MainSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = MainSession.PlatformSesstion[Constant.HospitalId];
            var time = MainSession.PlatformSesstion[Constant.SessionTime];

            var refererTemplate = $"https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

            return refererTemplate;
        }
    }
}
