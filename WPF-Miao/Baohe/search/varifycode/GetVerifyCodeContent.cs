using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using Base.viewModel;

namespace Baohe.search.varifycode
{
    internal class GetVerifyCodeContent : ContentBase
    {
        private ISessionItem SessionItem { get; }
        public GetVerifyCodeContent(string url, ISessionItem sessionItem) : base(url)
        {
            SessionItem = sessionItem;
        }

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];
            var deptId = BaoheSession.PlatformSesstion[Constant.DeptId];
            var defaultNumber = SessionBuilder.GetDefaultNumber(SessionItem);
            var doctorSn = defaultNumber["DoctorSN"];
            var arrangeId = defaultNumber["ArrangeID"];

            //https://appoint.yihu.com/appoint/register/registerOrder.html?platformType=9001026&hospitalId=1047063&deptId=7229244&doctorSn=711188785&arrangeId=160450569&utm_source=0.0.h.1026.bus010.0

            var refererTemplate = $"https://appoint.yihu.com/appoint/register/registerOrder.html?platformType={platformType}&hospitalId={hospitalId}&deptId={deptId}&doctorSn={doctorSn}&arrangeId={arrangeId}&utm_source=0.0.h.1026.bus010.0";

            return refererTemplate;
        }
    }
}
