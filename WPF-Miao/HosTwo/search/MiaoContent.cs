using HosTwo.common;
using HosTwo.login;
using HosTwo.session;
using Utils;

namespace HosTwo.search
{
    internal class MiaoContent : HosTwoContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/YuYue/OrderDocNoSources";

        public string Date { get; }
        public MiaoContent(HosTwoLogin user, string date) : base(baseUrl, user)
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
