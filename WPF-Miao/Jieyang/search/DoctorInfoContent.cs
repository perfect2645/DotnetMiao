using jieyang.common;
using jieyang.session;

namespace jieyang.search
{
    internal class DoctorInfoContent : jieyangBaseContent
    {
        public DoctorInfoContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.DoctorId);
        }
    }
}
