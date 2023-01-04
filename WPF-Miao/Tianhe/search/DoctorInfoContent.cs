using Tianhe.common;
using Tianhe.session;

namespace Tianhe.search
{
    internal class DoctorInfoContent : TianheContent
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
