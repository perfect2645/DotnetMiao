using Ych.common;
using Ych.session;

namespace Ych.search
{
    internal class SearchMiaoContent : YchContent
    {
        private const string url = "http://www.szychrmyy.com/wechatclient/api/auth/appointment/listNumber";

        public string Date { get; private set; }
        public SearchMiaoContent(string date) : base(url)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.OpenId);
        }
    }
}
