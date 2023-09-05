using Dayim.common;
using Dayim.login;
using Dayim.session;
using Utils;

namespace Dayim.search
{
    internal class VaccineContent : DayimContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/make/find/vaccine/by/hospital";

        public string Date { get; set; }

        public VaccineContent(DayimLogin user, string date) : base(baseUrl, user)
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
