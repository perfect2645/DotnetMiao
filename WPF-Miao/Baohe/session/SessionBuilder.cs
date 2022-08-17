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
            sb.BuildKeyValue(name, BaoheSession.PlatformSesstion[name]);
        }

        public static Dictionary<string, object> GetArrangeWater(ISessionItem sessionItem)
        {
            var arrangeWaterList = sessionItem.SessionDic[Constant.ArrangeWater] as List<Dictionary<string, object>>;

            return arrangeWaterList?.FirstOrDefault();
        }
    }
}
