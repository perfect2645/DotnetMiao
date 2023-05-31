using Dxm.common;
using Dxm.login;
using Dxm.session;
using Utils;

namespace Dxm.search
{
    internal class VaccineContent : DxmContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/make/find/vaccine/by/hospital";

        public string Date { get; set; }

        public VaccineContent(DxmLogin user, string date) : base(baseUrl, user)
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
