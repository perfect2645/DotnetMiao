using Zhi.common;
using Zhi.session;

namespace Zhi.search
{
    internal class UserContent : ZhiContent
    {
        private const string url = $"http://www.szZhirmyy.com/wechatclient/api/user/listFamily";

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
