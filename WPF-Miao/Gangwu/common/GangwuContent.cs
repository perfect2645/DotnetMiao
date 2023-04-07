using HttpProcessor.Content;
using System;
using System.Text;
using Utils.datetime;
using Utils.stringBuilder;

namespace Gangwu.common
{
    internal class GangwuContent : HttpStringContent
    {
        public GangwuContent(string url) : base(url)
        {
            ContentType = "application/json";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "jinshuju.net");
            AddHeader("Connection", "keep-alive");
            AddHeader("sec-ch-ua", "\"Not_A Brand\";v=\"99\", \"Google Chrome\";v=\"109\", \"Chromium\";v=\"109\"");
            AddHeader("Accept", "*/*");
            AddHeader("x-csrf-token", "");
            AddHeader("sec-ch-ua-mobile", "?0");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36");
            AddHeader("sec-ch-ua-platform", "\"Windows\"");
            AddHeader("Origin", "https://jinshuju.net");
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", $"https://jinshuju.net/f/MAp5Yn");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9");
            AddHeader("Cookie", BuildCookie());
        }

        private string BuildCookie()
        {
            var strBuilder = new StringBuilder();
            strBuilder.BuildKeyValue("jsj_uid", "e0dd33a0-ad25-4760-b17d-c572de7578ed");

            var timestamp = DateTimeUtil.GetTimeStamp();
            strBuilder.BuildKeyValue("start_filling_time_MAp5Yn", timestamp);

            strBuilder.BuildKeyValue("csrf_token", timestamp);

            return strBuilder.ToString();
        }
    }
}
