using Puzhou.common;
using Puzhou.login;
using Puzhou.session;
using Utils;

namespace Puzhou.search
{
    internal class VaccineContent : PuzhouContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/make/find/vaccine/by/hospital";

        public string Date { get; set; }

        public VaccineContent(PuzhouLogin user, string date) : base(baseUrl, user)
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
