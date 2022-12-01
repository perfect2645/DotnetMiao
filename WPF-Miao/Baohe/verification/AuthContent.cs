using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Baohe.verification
{
    internal class AuthContent : ContentBase
    {
        public AuthContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            var userDetail = BaoheSession.UserSession[Constant.MemberList] as List<Dictionary<string, object>>;
            var userName = userDetail[0].GetString("Cname");
            var memberSn = userDetail[0].GetString("Memberid");
            AddContent("name", UnicodeConverter.Encode(userName, true));
            AddContent("memberSn", memberSn);
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
