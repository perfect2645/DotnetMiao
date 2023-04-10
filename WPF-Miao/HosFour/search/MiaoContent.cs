using HosFour.common;
using HosFour.login;

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
            AddContent("docCode", "0400||儿科门诊普通号|儿科门诊|普通门诊");
            AddContent("dataSource", string.Empty);
            AddContent("day", Date);
            AddContent("t", 1);
        }
    }
}
