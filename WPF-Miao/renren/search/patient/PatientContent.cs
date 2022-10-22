using HttpProcessor.Content;
using renren.session;

namespace renren.search.patient
{
    internal class PatientContent : HttpStringContent
    {
        public PatientContent(string url) : base(url)
        {
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "www.medic.ren");
            AddHeader(MainSession.PlatformSesstion, Constants.MedicToken);
            AddHeader("Accept", "*/*");
            AddHeader("Authorization", "ddtoken");
            AddHeader("source", "wx");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Origin", "https://www.medic.ren");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Referer", "https://www.medic.ren/app/");
            AddHeader("Connection", "keep-alive");
        }

        private void BuildContent()
        {
            AddContent("originUserHospitalId", MainSession.PlatformSesstion[Constants.UserHospitalId]);
            AddContent("isTest", false);
            AddContent("hasPatients", true);
            AddContent("appId", "wx8320e743a5db7bff"); // https://www.medic.ren/PM-server/wechatAuthorizationInfo/getSignature 
        }
    }
}
