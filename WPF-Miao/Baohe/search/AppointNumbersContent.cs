using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Shapes;

namespace Baohe.search
{
    internal class AppointNumbersContent : ContentBase
    {
        public AppointNumbersContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
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
    }
}
