using Puzhou.common;
using Puzhou.login;
using Puzhou.session;
using Utils;

namespace Puzhou.search
{
    internal class MiaoContent : PuzhouContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/make/appointment/details";

        public string Date { get; set; }

        public MiaoContent(PuzhouLogin user, string date) : base(baseUrl, user)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            AddContent("vaccineInfoId", deptId);
            AddContent("hospitalCode", hosId);
            AddContent("makeAnAppointment", Date);
        }
    }
}
