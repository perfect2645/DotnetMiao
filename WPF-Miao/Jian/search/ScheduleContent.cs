using Jian.common;
using Jian.login;
using Jian.session;
using Utils;

namespace Jian.search
{
    internal class ScheduleContent : JianContent
    {
        private static string baseUrl = "https://app.gocent.com.cn/unite/api/Schedual/SchedualInfo?hospitalCode=";

        public string Date { get; set; }

        public ScheduleContent(JianLogin user, string date) : base(baseUrl, user)
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
