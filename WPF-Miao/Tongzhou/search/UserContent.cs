using Tongzhou.common;
using Tongzhou.login;

namespace Tongzhou.search
{
    internal class UserContent : TongzhouPostContent
    {
        public UserContent(TongzhouLogin user) : base("current", "account.baseUserMvcService", user)
        {
            BuildContent();
            BuildSignHeaders();
        }

        private void BuildContent()
        {

        }
    }
}
