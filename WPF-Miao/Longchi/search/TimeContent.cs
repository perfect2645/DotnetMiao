using Longchi.common;

namespace Longchi.search
{
    internal class TimeContent : LongchiContent
    {

        private static string url = "http://hpv_ym.zzytrj.net:15003/api/yuyue.php";
        public string Dizhi { get; set; }
        public TimeContent(string dizhi, string cookie) : base(url, cookie)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("cmd", "get_yuyue_date");
            AddContent("qty", 1);
            AddContent("dizhi", Dizhi);
        }
    }
}
