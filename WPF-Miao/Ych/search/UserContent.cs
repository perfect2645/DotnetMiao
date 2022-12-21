using Ych.common;
using Ych.session;

namespace Ych.search
{
    internal class UserContent : YchContent
    {
        private const string url = $"http://www.szychrmyy.com/wechatclient/api/user/listFamily";

        public UserContent() : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(Constants.OpenId, MainSession.OpenId);
        }
    }
}
