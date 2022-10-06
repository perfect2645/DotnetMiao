using HttpProcessor.Content;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace Jkchegu.appointment
{
    internal class AppointContent : HttpStringContent
    {
        public AppointContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "app.whkfqws.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "*/*");
            AddHeader("Origin", "http://app.whkfqws.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            var etid = JkSession.MiaoSession["Etid"].NotNullString();
            AddHeader("Referer", $"http://app.whkfqws.com/wx-mobile/Vaccination/vaccination.do?ETID={etid}");

            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            AddHeader("Accept-Encoding", "gzip, deflate");
        }

        private void BuildContent()
        {
            AddContent("APPOINTMENT_DATE", JkSession.MiaoSession["Date"]);
            AddContent("dateCount", JkSession.MiaoSession["Time"]);
            AddContent("ETID", "7bf4400434ea4e80a6dfb331f6f6a077");
            AddContent("VACCINES_NAME", "18");
            AddContent("DOC_CUSTOM_VACCINE_GUID", JkSession.MiaoSession["GUID"]);
            AddContent("code", JkSession.MiaoSession["Yzm"]);
        }
    }
}
