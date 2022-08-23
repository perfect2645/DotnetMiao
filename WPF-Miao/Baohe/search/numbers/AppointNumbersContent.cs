using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Shapes;

namespace Baohe.search.numbers
{
    internal class AppointNumbersContent : ContentBase
    {
        public AppointNumbersContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            var arrangeWater = SessionBuilder.GetAvailableArrangeWater();

            if (arrangeWater == null)
            {
                throw new HttpException($"{Constant.ProjectName}:查苗成功-没有可用苗", "no miao");
            }
            
            AddContent(Constant.ArrangeId, arrangeWater["ArrangeID"]);
            AddContent(BaoheSession.PlatformSesstion, Constant.HospitalId);
            AddContent(Constant.ChannelId, BaoheSession.PlatformSesstion[Constant.LoginChannel]);
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
