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
        public string UserName { get;set; }
        public string Tel { get; private set; }

        public string Phone { get; private set; }

        public string ArrangeId { get; private set; }

        public SendYzmContent(string url, string userName, string phone = "") : base(url)
        {
            UserName = userName;
            Phone = phone;
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            var accountSn = MainSession.UserSession[Constant.accountSn].NotNullString();

            var userDetail = SessionBuilder.GetDefaultMember(UserName);
            if (string.IsNullOrEmpty(Phone))
            {
                Tel = userDetail[Constant.Phone].NotNullString();
            }
            else
            {
                Tel = Phone;
            }

            Content.Add("tel", Tel);
            Content.Add(Constant.accountSn, accountSn);

            MainSession.UserSession[Constant.accountSn].NotNullString();
            Content.Add(Constant.ArrangeId, accountSn);
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
