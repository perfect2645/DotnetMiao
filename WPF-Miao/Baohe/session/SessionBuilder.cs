using Baohe.constants;
using Base.viewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static Dictionary<string, object> GetMaxArrangeWater(ISessionItem sessionItem)
        {
            var arrangeWaterList = sessionItem.SessionDic[Constant.ArrangeWater] as List<Dictionary<string, object>>;
            var availableWater = arrangeWaterList?.MaxBy(x => x["availablenum"].ToString()!.ToLong());
            return availableWater;
        }

        public static Dictionary<string, object> GetDefaultNumber(ISessionItem sessionItem)
        {
            var result = sessionItem.SessionDic[Constant.Numbers] as List<Dictionary<string, object>>;

            return result?.LastOrDefault();
        }

        public static Dictionary<string, object> GetDefaultMember(ISessionItem sessionItem)
        {
            var result = sessionItem.SessionDic[Constant.MemberList] as List<Dictionary<string, object>>;

            return result?.FirstOrDefault();
        }

        public static Dictionary<string, object> GetDefaultDoctor()
        {
            var result = BaoheSession.PlatformSesstion[Constant.DoctorList] as List<Dictionary<string, object>>;

            return result?.FirstOrDefault();
        }
    }
}
