using NLog.LayoutRenderers;
using Ych.common;
using Ych.session;

namespace Ych.search
{
    internal class UserContent : YchContent
    {
        private const string _baseUrl = "/wechatclient/api/user/listFamily";
        public UserContent() : base(_baseUrl)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(Constants.OpenId, MainSession.OpenId);
        }
    }
}
