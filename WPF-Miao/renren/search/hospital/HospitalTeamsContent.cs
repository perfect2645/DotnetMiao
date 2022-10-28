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
            AddContent(MainSession.PlatformSession, Constants.HospitalId);
            AddContent(MainSession.PlatformSession, Constants.UserHospitalId);
        }
    }
}
