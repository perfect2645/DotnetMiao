using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Shapes;

namespace Baohe.search.numbers
{
    internal class AppointNumbersContent : ContentBase
    {
        private Dictionary<string, object> ArrangeWater { get; }

        public AppointNumbersContent(string url, Dictionary<string, object> arrangeWater) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            ArrangeWater = arrangeWater;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(Constant.ArrangeId, ArrangeWater["ArrangeID"]);
            AddContent(BaoheSession.PlatformSesstion, Constant.HospitalId);
            //AddContent(Constant.ChannelId, BaoheSession.PlatformSesstion[Constant.LoginChannel]);
            AddContent("appliedDepartment", "");
            AddContent(Constant.ChannelId, BaoheSession.PlatformSesstion[Constant.PlatformType]);
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
