using Anze.common;
using Anze.login;
using Anze.session;
using Utils;

namespace Anze.search
{
    internal class MiaoContent : AnzeContent
    {
        private static string baseUrl = "https://yuyue.azjkzx.cn/api/index/yimiaolist?ids=1";

        public MiaoContent(AnzeLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
        }
    }
}
