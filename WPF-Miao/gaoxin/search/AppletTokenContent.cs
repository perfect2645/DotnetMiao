using gaoxin.common;
using gaoxin.session;

namespace gaoxin.search
{
    internal class AppletTokenContent : GaoxinContent
    {
        public AppletTokenContent(string url, GaoxinLogin login) : base(url, "获取token", login)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            //AddContent("code", MainSession.Code);
        }
    }
}
