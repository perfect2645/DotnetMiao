using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using System.Collections.Generic;
using Utils;
using Utils.stringBuilder;

namespace Baohe.verification
{
    internal class SendYzmContent : ContentBase
    {
        public string Tel { get; private set; }

        public SendYzmContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            var accountSn = BaoheSession.UserSession[Constant.accountSn].NotNullString();

            var userDetail = SessionBuilder.GetDefaultMember();
            Tel = userDetail[Constant.Phone].NotNullString();

            Content.Add("tel", Tel);
            Content.Add(Constant.accountSn, accountSn);
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
