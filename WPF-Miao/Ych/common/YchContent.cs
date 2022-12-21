using HttpProcessor.Content;
using System;
using System.Linq;
using Utils;
using Utils.stringBuilder;
using Ych.session;

namespace Ych.common
{
    internal class YchContent : HttpStringContent
    {
        public YchContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        protected virtual void BuildHeader()
        {
            AddHeader("Host", "www.szychrmyy.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("Origin", "http://www.szychrmyy.com");
            AddHeader("openId", MainSession.OpenId);
            AddHeader("X-Access-Token", "e90770a107724c8ba1fb42eb25b88190");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Referer", $"http://www.szychrmyy.com/wechatclient/?openId={MainSession.OpenId}");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }


        protected string GetContentsMD5()
        {
            try
            {
                var contentValues = Content.Values.Select(x => x.NotNullString()).ToArray();
                var contentValuesString = string.Join(string.Empty, contentValues);

                var contentsMD5 = Encryptor.ToMD5String(contentValuesString).ToLower();

                return contentsMD5;
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, ex.Message);
                return String.Empty;
            }
        }
    }
}
