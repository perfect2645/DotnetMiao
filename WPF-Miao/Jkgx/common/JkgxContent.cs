using Jkgx.login;
using HttpProcessor.Content;

namespace Jkgx.common
{
    internal class JkgxContent : HttpStringContent
    {
        public JkgxLogin User { get; private set; }

        public JkgxContent(string baseUrl, JkgxLogin user) : base(baseUrl)
        {
            User = user;

            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "mp.dywrbt.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("apptype", "DYM");
            AddHeader("birthdayDate", string.Empty);
            AddHeader("id", User.TokenId);




            AddHeader("Referer", "https://servicewechat.com/wx1cf50c2a73c08a5d/35/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
