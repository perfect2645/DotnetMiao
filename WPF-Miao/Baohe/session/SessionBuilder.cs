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
    }
}
