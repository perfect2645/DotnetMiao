using Ych.common;
using Ych.session;

namespace Ych.search
{
    internal class UserContent : YchContent
    {
        public UserContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(Constants.OpenId, MainSession.OpenId);
        }
    }
}
