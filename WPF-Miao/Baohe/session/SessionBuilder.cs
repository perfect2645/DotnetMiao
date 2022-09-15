using Baohe.constants;
using Base.viewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.datetime;
using Utils.stringBuilder;

namespace Baohe.session
{
    internal static class SessionBuilder
    {
        public static void BuildSession(this StringBuilder sb, string name)
        {
            if (BaoheSession.PlatformSesstion.ContainsKey(name))
            {
                sb.BuildKeyValue(name, BaoheSession.PlatformSesstion[name]);
            }
        }

        public static List<Dictionary<string, object>> GetAvailableArrangeWater()
        {
            var arrangeWaterList = BaoheSession.MiaoSession[Constant.ArrangeWater] as List<Dictionary<string, object>>;
            var availableWater = arrangeWaterList?.Where(x => x["OverTime"].NotNullString().ToLong() == 0
            && x["availablenum"].NotNullString().ToLong() > 0
                && DateTimeUtil.IsEqualOrGreaterThanToday(x["InvalidDate"].NotNullString())).ToList();

            return availableWater;
        }

        public static List<Dictionary<string, object>> GetAvailableArrangeWater(List<Dictionary<string, object>> originalWaters)
        {
            var availableWater = originalWaters?.Where(x => x["OverTime"].NotNullString().ToLong() == 0
            && x["availablenum"].NotNullString().ToLong() > 0
                && DateTimeUtil.IsEqualOrGreaterThanToday(x["InvalidDate"].NotNullString())).ToList();

            return availableWater;
        }

        public static Dictionary<string, object> GetDefaultMember()
        {
            var result = BaoheSession.UserSession[Constant.MemberList] as List<Dictionary<string, object>>;

            return result?.FirstOrDefault();
        }

        public static Dictionary<string, object> GetDefaultDoctor()
        {
            var result = BaoheSession.MiaoSession[Constant.DoctorList] as List<Dictionary<string, object>>;

            return result?.FirstOrDefault();
        }
    }
}
