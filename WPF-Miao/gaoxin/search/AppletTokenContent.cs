using gaoxin.common;
using gaoxin.session;

namespace gaoxin.search
{
    internal class AppletTokenContent : GaoxinContent
    {
        public AppletTokenContent(string url) : base(url, "获取token")
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, "code");
        }
    }
}
