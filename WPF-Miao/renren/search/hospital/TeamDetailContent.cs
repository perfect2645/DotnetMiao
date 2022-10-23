using renren.common;
using renren.session;

namespace renren.search.hospital
{
    internal class TeamDetailContent : RenrenBaseContent
    {
        public TeamDetailContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.HospitalSession, Constants.TeamId);
            AddContent("role", 1);
            AddContent(MainSession.PlatformSesstion, Constants.UserHospitalId);
        }
    }
}
