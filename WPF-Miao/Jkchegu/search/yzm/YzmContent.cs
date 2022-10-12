using HttpProcessor.Content;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace Jkchegu.search.yzm
{
    internal class YzmContent : HttpStringContent
    {
        public YzmContent(string url) : base(url)
        {
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "app.whkfqws.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "image/webp,image/apng,image/*,*/*;q=0.8");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            var etid = JkSession.MiaoSession["Etid"].NotNullString();
            AddHeader("Referer", $"http://app.whkfqws.com/wx-mobile/Vaccination/vaccination.do?ETID={etid}");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            AddHeader("Accept-Encoding", "gzip, deflate");
        }
    }
}
