using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Baohe.search.ArrangeWater
{
    internal class ArrangeWaterContent : ContentBase
    {
        public ArrangeWaterContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            //doctorSn=711111254&hospitalId=1040231&channelId=9000370
            var targetDoctor = SessionBuilder.GetDefaultDoctor();

            if (targetDoctor == null)
            {
                Logging.GLog.Logger.Warn("ArrangeWaterContent targetDoctor is null");
                return;
            }
            Content.Add(Constant.DoctorSn, targetDoctor[Constant.DoctorSn]);
            Content.Add(Constant.HospitalId, MainSession.PlatformSesstion[Constant.HospitalId]);
            Content.Add(Constant.ChannelId, MainSession.PlatformSesstion[Constant.PlatformType]);
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
