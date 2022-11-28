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
            var doctorList = BaoheSession.MiaoSession[Constant.DoctorList] as List<Dictionary<string, object>>;
            var targetDoctor = doctorList?.FirstOrDefault(d => IsTargetDoctor(d));
            if (targetDoctor == null)
            {
                targetDoctor = doctorList?.FirstOrDefault();
            }
            if (targetDoctor == null)
            {
                Logging.GLog.Logger.Warn("ArrangeWaterContent targetDoctor is null");
                return;
            }
            Content.Add(Constant.DoctorSn, targetDoctor[Constant.DoctorSn]);
            Content.Add(Constant.HospitalId, BaoheSession.PlatformSesstion[Constant.HospitalId]);
            Content.Add(Constant.ChannelId, BaoheSession.PlatformSesstion[Constant.PlatformType]);
        }

        private bool IsTargetDoctor(Dictionary<string, object> doctor)
        {
            var doctorSn = doctor.GetString(Constant.DoctorSn);
            if (string.IsNullOrEmpty(doctorSn))
            {
                return false;
            }

            var sessionDoctorSn = BaoheSession.PlatformSesstion.GetString(Constant.DoctorSn);
            if (doctorSn.Equals(sessionDoctorSn))
            {
                return true;
            }

            return false;
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
