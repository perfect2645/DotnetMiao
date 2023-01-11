using Xihongmen.common;
using Xihongmen.login;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class UserContent : XhmContent
    {
        private const string url = "https://yiyuan.dabannet.cn/getChildList";

        public UserContent(XhmLogin user) : base(url)
        {
            BuildContent(user);
        }

        private void BuildContent(XhmLogin user)
        {
            AddContent("key", Key);
            AddContent("token", user.Token);
        }
    }
}
