using HttpProcessor.Content;
using Jkchegu.session;
using Utils.stringBuilder;

namespace Jkchegu.appointment
{
    internal class AppointContent : HttpStringContent
    {
        private const string Url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_save.do";

        public Order Order { get; set; }
        public string Etid { get; }

        public AppointContent(string etid, Order order) : base(Url)
        {
            Etid = etid ?? JkSession.PlatformSession["Etid"].NotNullString();
            Order = order;
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
            var etid = Etid;
            AddHeader("Referer", $"http://app.whkfqws.com/wx-mobile/Vaccination/vaccination.do?ETID={etid}");

            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Cookie", Order.User.Session);
        }

        private void BuildContent()
        {
            AddContent("APPOINTMENT_DATE", Order.Date);
            AddContent("dateCount", Order.Time);
            AddContent("ETID", Etid);
            AddContent("VACCINES_NAME", JkSession.PlatformSession[Constants.DeptId]);
            AddContent("DOC_CUSTOM_VACCINE_GUID", Order.GUID);
            AddContent("code", Order.Yzm);
        }
    }
}
