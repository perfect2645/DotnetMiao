using Xihongmen.common;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class UserContent : XhmContent
    {
        private const string url = "https://yiyuan.dabannet.cn/getChildList";

        public UserContent() : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("key", Key);
            AddContent("token", MainSession.Token);
        }
    }
}
