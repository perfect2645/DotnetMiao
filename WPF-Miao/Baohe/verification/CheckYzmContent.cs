using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using System.Collections.Generic;
using Utils.stringBuilder;

namespace Baohe.verification
{
    internal class CheckYzmContent : ContentBase
    {
        public string Tel { get; private set; }
        public string Yzm { get; private set; }

        public CheckYzmContent(string url, string yzm) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";

            Yzm = yzm;
            BuildContent();
        }

        private void BuildContent()
        {
            var accountSn = BaoheSession.UserSession[Constant.accountSn].NotNullString();

            var userDetail = BaoheSession.UserSession[Constant.MemberList] as List<Dictionary<string, object>>;
            Tel = userDetail[0][Constant.Phone].NotNullString();

            Content.Add("tel", Tel);
            Content.Add("yzmCode", Yzm);
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
