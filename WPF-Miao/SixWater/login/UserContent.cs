using SixWater.common;

namespace SixWater.login
{
    internal class UserContent : SixWaterContent
    {
        private static string baseUrl = "https://oss.zsqrmyy.com:8443/patient/familyMember/own?desensitization=true";
        public UserContent(SixWaterLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
        }
    }
}
