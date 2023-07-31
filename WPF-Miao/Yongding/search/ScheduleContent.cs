using Yongding.common;
using Yongding.login;
using Yongding.session;
using Utils;

namespace Yongding.search
{
    internal class ScheduleContent : YongdingContent
    {
        private static string baseUrl = "http://yiliao2.lefeiniu.com:8081/resource/get_list?id=8&wek=4&riqi=2023-08-03";

        public ScheduleContent(YongdingLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            AddContent("vaccineId", deptId);
            AddContent("vaccineAddressId", hosId);
            AddContent("token", User.Token);
        }
    }
}
