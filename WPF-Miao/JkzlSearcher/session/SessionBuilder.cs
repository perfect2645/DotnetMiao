using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using Utils.stringBuilder;

namespace JkzlSearcher.session
{
    internal static class SessionBuilder
    {
        public static void BuildSession(this StringBuilder sb, string name)
        {
            if (MainSession.PlatformSession.ContainsKey(name))
            {
                sb.BuildKeyValue(name, MainSession.PlatformSession[name]);
            }
        }

        public static Dictionary<string, object> GetDefaultMember()
        {
            var result = MainSession.PlatformSession["MemberList"] as List<Dictionary<string, object>>;

            return result?.LastOrDefault();
        }

        public static bool IsTargetDoctor(Dictionary<string, object> doctor)
        {
            var doctorSn = doctor.GetString(Constants.DoctorSn);
            if (string.IsNullOrEmpty(doctorSn))
            {
                return false;
            }

            var sessionDoctorSn = MainSession.PlatformSession.GetString(Constants.DoctorSn);
            if (doctorSn.Equals(sessionDoctorSn))
            {
                return true;
            }

            return false;
        }
    }
}
