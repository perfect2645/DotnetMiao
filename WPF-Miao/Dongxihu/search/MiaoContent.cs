using Dongxihu.common;
using Dongxihu.login;
using Dongxihu.session;
using Utils;

namespace Dongxihu.search
{
    internal class MiaoContent : DongxihuContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/YuYue/OrderDocNoSources";

        public string Date { get; }
        public MiaoContent(DongxihuLogin user, string date) : base(baseUrl, user)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            var docCode = MainSession.PlatformSession.GetString(Constants.DocCode);
            var docCodeEncode = UnicodeConverter.EncodeOriginal(docCode, true);
            AddContent("docCode", docCodeEncode);
            AddContent("dataSource", string.Empty);
            AddContent("day", Date);
            AddContent("t", 1);
        }
    }
}
