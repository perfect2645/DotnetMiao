using Redhouse.common;
using Redhouse.session;
using Utils;

namespace Redhouse.search
{
    internal class WechatUserContent : RedhouseContent
    {
        private const string url = $"https://wxgzh.fckyy.org.cn/api/hosservice/WeChat/GetWeChatUser";

        public WechatUserContent() : base(url)
        {
            InitContent();
        }

        private void InitContent()
        {
            Contents.AddOrUpdate(MainSession.PlatformSession, Constants.HosId);
        }
    }
}
