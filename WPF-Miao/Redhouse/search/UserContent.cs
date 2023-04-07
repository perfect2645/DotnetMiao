using Redhouse.common;
using Redhouse.session;
using Utils;

namespace Redhouse.search
{
    internal class UserContent : RedhouseContent
    {
        private const string url = $"https://wxgzh.fckyy.org.cn/api/hosservice/WeChat/GetUserInfo";

        public UserContent() : base(url)
        {
            InitContent();
        }

        private void InitContent()
        {
            Contents.AddOrUpdate(MainSession.PlatformSession, Constants.HosId);
        }
    }
}
