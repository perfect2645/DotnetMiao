using jieyang.common;
using jieyang.session;

namespace jieyang.search
{
    internal class SearchMiaoContent : JieyangBaseContent
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
