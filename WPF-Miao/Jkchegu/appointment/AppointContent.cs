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
        public User User { get; }

        public AppointContent(User user, Order order) : base(Url)
        {
            User = user;
            Etid = user.Etid ?? JkSession.PlatformSession["Etid"].NotNullString();
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
            AddHeader("Cookie", User.Session);
        }

        private void BuildContent()
        {
            AddContent("APPOINTMENT_DATE", "2023-04-17");
            AddContent("dateCount", Order.Time);
            AddContent("ETID", "bd208cd181694e66b930a5f9b23fbd11");
            AddContent("VACCINES_NAME", JkSession.PlatformSession[Constants.DeptId]);
            AddContent("DOC_CUSTOM_VACCINE_GUID", "e2aef03aa00c4d8f9edab967f3cd10f2");
            AddContent("code", Order.Yzm);
        }
    }
}
