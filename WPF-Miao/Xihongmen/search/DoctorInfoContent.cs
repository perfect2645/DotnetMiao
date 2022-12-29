using Xihongmen.common;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class DoctorInfoContent : XihongmenBaseContent
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
