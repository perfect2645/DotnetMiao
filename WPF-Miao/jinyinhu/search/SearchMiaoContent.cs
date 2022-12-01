using jinyinhu.common;
using jinyinhu.session;

namespace jinyinhu.search
{
    internal class SearchMiaoContent : JinyinhuContent
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
