using HttpProcessor.Content;

namespace Longchi.common
{
    internal class LongchiContent : HttpStringContent
    {

        public string Cookie { get; private set; }
        public string FamilyId { get; private set; }
        public LongchiContent(string url, string cookie, string familyId = "112775") : base(url)
        {
            //ContentType = "application/x-www-form-urlencoded";
            Cookie = cookie;
            FamilyId = familyId;
            BuildHeader();
        }

        protected virtual void BuildHeader()
        {
            AddHeader("Host", "hpv_ym.zzytrj.net:15003");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "http://hpv_ym.zzytrj.net:15003");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Cookie", Cookie);
            AddHeader("Referer", $"http://hpv_ym.zzytrj.net:15003/views/yuyue/yuyue_gr.html?id={FamilyId}");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
