using gaoxin.common;
using gaoxin.session;
using Utils;

namespace gaoxin.search
{
    internal class UserContent : GaoxinContent
    {
        public UserContent(string url) : base(url, "获取user")
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("userId", MainSession.PlatformSession.GetString(Constants.UserId));
        }
    }
}
