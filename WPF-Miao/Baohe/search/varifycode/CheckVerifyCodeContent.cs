using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.search.varifycode
{
    internal class CheckVerifyCodeContent : ContentBase
    {
        private ISessionItem SessionItem { get; }
        public CheckVerifyCodeContent(string url, ISessionItem sessionItem) : base(url)
        {
            SessionItem = sessionItem;
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            Content.Add("inputCode", SessionItem.SessionDic[Constant.GraphAuthCode]);
        }

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];
            var deptId = BaoheSession.PlatformSesstion[Constant.DeptId];
            var defaultNumber = SessionBuilder.GetDefaultNumber(SessionItem);
            var doctorSn = defaultNumber["DoctorSN"];
            var arrangeId = defaultNumber["ArrangeID"];

            var refererTemplate = $"https://appoint.yihu.com/appoint/register/registerOrder.html?platformType={platformType}&hospitalId={hospitalId}&deptId={deptId}&doctorSn={doctorSn}&arrangeId={arrangeId}&utm_source=0.0.h.1026.bus010.0";

            return refererTemplate;
        }
    }
}
