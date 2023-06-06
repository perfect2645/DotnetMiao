using B114.common;
using B114.login;
using B114.session;
using Utils;

namespace B114.search
{
    internal class VaccineContent : B114Content
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/make/find/vaccine/by/hospital";

        public string Date { get; set; }

        public VaccineContent(B114Login user, string date) : base(baseUrl, user)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("hospitalCode", hosId);
            AddContent("makeAnAppointment", Date);
        }
    }
}
