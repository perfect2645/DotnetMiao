using HttpProcessor.Content;
using Tjzs.appointment;

namespace Tjzs.common
{
    internal class TjzsContent : HttpStringContent
    {
        public Order Order { get; private set; }
        public TjzsContent(string url, Order order) : base(url)
        {
            ContentType = "application/json";
            Order = order;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "tjzsprod.tsxcx.xyz");
            AddHeader("Connection", "keep-alive");
            AddHeader("Authorization", Order.Authorization);
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("Referer", $"https://health-cn.xyz/");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
