using HttpProcessor.Content;
using renren.session;

namespace renren.search.hospital
{
    internal class HospitalTeamsContent : HttpStringContent
    {
        public HospitalTeamsContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSesstion, Constants.HospitalId);
            AddContent(MainSession.PlatformSesstion, Constants.UserHospitalId);
        }
    }
}
