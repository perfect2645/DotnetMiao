using renren.common;
using renren.session;

namespace renren.search.patient
{
    internal class UserContent : RenrenBaseContent
    {
        public UserContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSesstion, Constants.OpenId);
            AddContent(MainSession.PlatformSesstion, Constants.HospitalId);
        }
    }
}
