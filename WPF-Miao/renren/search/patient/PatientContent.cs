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
            AddContent(MainSession.PlatformSesstion, Constants.AppId);
            AddContent(Constants.Mobile, MainSession.UserSession[Constants.Mobile]);
            AddContent("hasRedpack", false);
            AddContent(MainSession.PlatformSesstion, Constants.UserHospitalId);
            AddContent(Constants.PublicKey, MainSession.UserSession[Constants.PublicKey]);
            AddContent("hasTemporaryPatient", false);
            AddContent(Constants.UserId, MainSession.UserSession[Constants.UserId]);
            AddContent("presigned", true);
            AddContent("hasNewBpData", false);
            AddContent(MainSession.PlatformSesstion, Constants.OpenId);
            AddContent(MainSession.PlatformSesstion, Constants.HospitalId);
            AddContent("image", string.Empty);
            AddContent("nickname", string.Empty);
            AddContent("sex", "女");
            AddContent("tmCount", 0);
        }
    }
}
