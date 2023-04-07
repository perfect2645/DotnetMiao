using chutian.common;
using chutian.session;

namespace chutian.search
{
    internal class DoctorInfoContent : ChutianBaseContent
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
