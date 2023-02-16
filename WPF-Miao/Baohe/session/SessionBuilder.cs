using Baohe.constants;
using Base.viewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Baohe.session
{
    internal static class SessionBuilder
    {
        public static void BuildSession(this StringBuilder sb, string name)
        {
            if (MainSession.PlatformSesstion.ContainsKey(name))
            {
                sb.BuildKeyValue(name, MainSession.PlatformSesstion[name]);
            }
        }

        public static List<Dictionary<string, object>> GetAvailableArrangeWater()
        {
            var arrangeWaterList = MainSession.MiaoSession[Constant.ArrangeWater] as List<Dictionary<string, object>>;
            var availableWater = arrangeWaterList?.Where(x => x["OverTime"].NotNullString().ToLong() == 0
            && x["availablenum"].NotNullString().ToLong() > 0
                && DateTimeUtil.IsEqualOrGreaterThanToday(x["InvalidDate"].NotNullString())).ToList();

            return availableWater ?? new List<Dictionary<string, object>>();
        }

        public static List<Dictionary<string, object>> GetAvailableArrangeWater(List<Dictionary<string, object>> originalWaters)
        {
            var availableWater = originalWaters?.Where(x => x["OverTime"].NotNullString().ToLong() == 0
            && x["availablenum"].NotNullString().ToLong() > 0
                && DateTimeUtil.IsEqualOrGreaterThanToday(x["InvalidDate"].NotNullString())).ToList();

            return availableWater;
        }

        public static Dictionary<string, object> GetDefaultMember(string userName)
        {
            var result = MainSession.UserSession[Constant.MemberList] as List<Dictionary<string, object>>;

            if (!result.HasItem())
            {
                return null;
            }

            var matchedUser = result.FirstOrDefault(x => x["Cname"].NotNullString() == userName);
            if (matchedUser == null)
            {
                matchedUser = result?.LastOrDefault();
            }


            return matchedUser;
        }

        public static Dictionary<string, object> GetDefaultDoctor()
        {
            var doctorList = MainSession.MiaoSession[Constant.DoctorList] as List<Dictionary<string, object>>;

            var targetDoctor = doctorList?.FirstOrDefault(d => IsTargetDoctor(d));

            return targetDoctor ?? doctorList?.FirstOrDefault();
        }

        public static bool IsTargetDoctor(Dictionary<string, object> doctor)
        {
            var doctorSn = doctor.GetString(Constant.DoctorSn);
            if (string.IsNullOrEmpty(doctorSn))
            {
                return false;
            }

            var sessionDoctorSn = MainSession.PlatformSesstion.GetString(Constant.DoctorSn);
            if (doctorSn.Equals(sessionDoctorSn))
            {
                return true;
            }

            return false;
        }
    }
}
