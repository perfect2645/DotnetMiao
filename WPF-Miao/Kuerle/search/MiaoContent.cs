using Kuerle.common;
using Kuerle.login;
using Kuerle.session;
using Utils;

namespace Kuerle.search
{
    internal class MiaoContent : KuerleContent
    {
        private static string baseUrl = "https://yuyue.azjkzx.cn/api/index/yimiaolist?ids=1";

        public MiaoContent(KuerleLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
        }
    }
}
