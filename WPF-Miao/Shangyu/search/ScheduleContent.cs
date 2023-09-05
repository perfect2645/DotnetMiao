using Shangyu.common;
using Shangyu.login;
using Shangyu.session;
using Utils;

namespace Shangyu.search
{
    internal class ScheduleContent : ShangyuContent
    {
        private static string baseUrl = "https://app.gocent.com.cn/unite/api/Schedual/SchedualInfo?hospitalCode=";

        public string Date { get; set; }

        public ScheduleContent(ShangyuLogin user, string date) : base(baseUrl, user)
        {
            Date= date;
            var dept2 = MainSession.PlatformSession.GetString(Constants.DeptId);

            RequestUrl = $"{BaseUrl}&dept2={dept2}";
            BuildContent();
        }

        private void BuildContent()
        {
        }
    }
}
