using Sanya.common;
using Sanya.session;
using Utils;

namespace Sanya.login
{
    internal class LoginContent : SanyaContent
    {
        private static string baseUrl = "https://public.health.zoenet.cn/medical/internet-hospital/hospital-micro-homepage?";
        public LoginContent(SanyaLogin user) : base(baseUrl, user)
        {
            BuildUrl();
        }

        private void BuildUrl()
        {
            var orgCode = MainSession.PlatformSession.GetString(Constants.OrgCode);
            var zoneCode = MainSession.PlatformSession.GetString(Constants.ZoneCode);
            var zoneName = MainSession.PlatformSession.GetString(Constants.ZoneName);

            RequestUrl = $"{baseUrl}orgCode={orgCode}&zoneCode={zoneCode}&zoneName={zoneName}";
        }
    }
}
