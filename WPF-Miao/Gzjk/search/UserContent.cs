using Gzjk.common;
using Gzjk.login;

namespace Gzjk.search
{
    internal class UserContent : GzjkContent
    {
        private static string baseUrl = "https://api.cn2030.com/sc/wx/HandlerSubscribe.ashx?act=User";
        public UserContent(GzjkLogin user) : base(baseUrl, user)
        {
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("zftsl", User.Zftsl);
        }
    }
}
