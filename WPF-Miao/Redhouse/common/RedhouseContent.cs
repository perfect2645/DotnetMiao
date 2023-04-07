using HttpProcessor.Content;
using Redhouse.Encrypt;
using Redhouse.session;
using System;
using System.Collections.Generic;

namespace Redhouse.common
{
    internal class RedhouseContent : HttpStringContent
    {
        public Dictionary<string, object> Contents { get; set; }

        public RedhouseContent(string url) : base(url)
        {
            Contents = new Dictionary<string, object>();
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "www.Redhouse.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/javascript, */*");
            AddHeader("Origin", "http://www.Redhouse.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Authorization", MainSession.Authorization);
            AddHeader("Referer", "http://www.Redhouse.com/home/");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }

        public void BuildContent()
        {
            try
            {
                var redhouseData = HfzEncrypt.Encrypt(Contents);

                AddContent("encryKey", redhouseData.encryKey);
                AddContent("data", redhouseData.data);
                AddContent("sign", redhouseData.sign);
                AddContent("timestamp", redhouseData.timestamp);
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, ex.Message);
            }
        }
    }
}
