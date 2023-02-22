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
        public string UserName { get; set; }
        public AuthContent(string url, string userName) : base(url)
        {
            UserName = userName;
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            var userDetail = SessionBuilder.GetDefaultMember(UserName);
            var userName = userDetail.GetString("Cname");
            var memberSn = userDetail.GetString("Membersn");
            AddContent("name", UnicodeConverter.Encode(userName, true));
            AddContent("memberSn", memberSn);
        }

        public string BuildReferer()
        {
            var platformType = MainSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = MainSession.PlatformSesstion[Constant.HospitalId];
            var time = MainSession.PlatformSesstion[Constant.SessionTime];

            var refererTemplate = $"https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

            return refererTemplate;
        }
    }
}
