using HosFour.common;
using HosFour.login;
using HosFour.session;
using Utils;

namespace HosFour.search
{
    internal class MiaoContent : HosFourContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/YuYue/OrderDocNoSources";

        public string Date { get; }
        public MiaoContent(HosFourLogin user, string date) : base(baseUrl, user)
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
