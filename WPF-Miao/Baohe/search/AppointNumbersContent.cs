using Baohe.constants;
using Baohe.session;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Shapes;

namespace Baohe.search
{
    internal class AppointNumbersContent : HttpStringContent
    {
        public AppointNumbersContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            AddHeader("Host", "appoint.yihu.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "https://appoint.yihu.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36");
            //AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");

            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            base.BuildDefaultHeaders(httpClient);
        }

        private void BuildContent()
        {
            AddContent("arrangeId", "160023903");
            AddContent("hospitalId", "1040231");
            AddContent("appliedDepartment", "");
            AddContent("channelId", "9000370");
            AddContent("ClinicCard", "");
        }

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];
            var time = BaoheSession.PlatformSesstion[Constant.SessionTime];

            var refererTemplate = $"https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

            return refererTemplate;
        }

        public string BuildCookie()
        {
            var sb = new StringBuilder();
            sb.BuildSession(Constant.YihuOpenId);
            sb.BuildSession(Constant.LoginType);
            sb.BuildSession(Constant.LoginProvinceiId);
            sb.BuildSession(Constant.LoginCityId);
            sb.BuildSession(Constant.LoginId);
            sb.BuildSession(Constant.OpenId);
            sb.BuildSession(Constant.BaseDoctorUid);
            sb.BuildSession(Constant.BaseUserType);
            sb.BuildSession(Constant.LoginChannel);
            sb.BuildSession(Constant.YiHuUserJosn);

            return sb.ToString();
        }
    }
}
