using HttpProcessor.Content;
using JkzlSearcher.session;
using Utils.datetime;

namespace JkzlSearcher.search
{
    internal class HosDeptContent : HttpStringContent
    {

        private const string Url = "https://appoint.yihu.com/appoint/do/dept/getHosDeptList";
        public HosDeptContent() : base(Url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
            BuildContent();
        }


        private void BuildHeader()
        {
            AddHeader("Host", "appoint.yihu.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "https://appoint.yihu.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36");
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            var referer = "https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType=1000031";
            AddHeader("Referer", referer);
        }

        private void BuildContent()
        {
            AddContent("t", DateTimeUtil.GetTimeStamp());
            AddContent("hospitalId", MainSession.GetNextHospitalId());
            AddContent("cooperate", "1");
            AddContent("orderColumns", "orderId%3A0");
            AddContent("channelId", "1000031");
        }

    }
}
