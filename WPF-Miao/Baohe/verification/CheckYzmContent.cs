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
        public string UserName { get; private set; }

        public CheckYzmContent(string url, string yzm, string userName) : base(url)
        {
            UserName = userName;
            ContentType = "application/x-www-form-urlencoded";

            Yzm = yzm;
            BuildContent();
        }

        private void BuildContent()
        {
            var accountSn = MainSession.UserSession[Constant.accountSn].NotNullString();

            var userDetail = SessionBuilder.GetDefaultMember(UserName);
            Tel = userDetail[Constant.Phone].NotNullString();

            Content.Add("tel", Tel);
            Content.Add("yzmCode", Yzm);
            Content.Add(Constant.accountSn, accountSn);
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
