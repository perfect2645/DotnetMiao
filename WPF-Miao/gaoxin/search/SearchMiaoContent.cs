using gaoxin.common;
using gaoxin.session;

namespace gaoxin.search
{
    internal class SearchMiaoContent : GaoxinContent
    {
        public SearchMiaoContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.DoctorId);
        }
    }
}
