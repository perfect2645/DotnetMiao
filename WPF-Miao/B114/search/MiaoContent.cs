using B114.common;
using B114.login;
using B114.session;
using Utils;

namespace B114.search
{
    internal class MiaoContent : B114Content
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/make/appointment/details";

        public string Date { get; set; }

        public MiaoContent(B114Login user, string date) : base(baseUrl, user)
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
