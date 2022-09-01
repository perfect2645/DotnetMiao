using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace Baohe.verification
{
    internal class SendYzmContent : ContentBase
    {
        public SendYzmContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            var accountSn = BaoheSession.UserSession[Constant.accountSn].NotNullString();
            var tel = BaoheSession.UserSession[Constant.Phone].NotNullString();

            Content.Add(Constant.DoctorSn, targetDoctor[Constant.DoctorSn]);
            Content.Add(Constant.HospitalId, BaoheSession.PlatformSesstion[Constant.HospitalId]);
            Content.Add("channelId", BaoheSession.PlatformSesstion[Constant.LoginChannel]);
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
