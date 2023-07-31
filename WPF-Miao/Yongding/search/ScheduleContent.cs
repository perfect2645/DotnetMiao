using Base.model;
using Utils;
using Yongding.common;
using Yongding.login;
using Yongding.session;

namespace Yongding.search
{
    internal class ScheduleContent : YongdingContent
    {
        private static string baseUrl = "http://yiliao2.lefeiniu.com:8081/resource/get_list?";
        public DspVal Date { get; set; }

        public ScheduleContent(YongdingLogin user, DspVal date) : base(baseUrl, user)
        {
            Date = date;

            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            RequestUrl = $"{baseUrl}id={deptId}&wek={Date.Value}&riqi={Date.Display}";
        }
    }
}
