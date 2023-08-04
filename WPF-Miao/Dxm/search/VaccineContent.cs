using Dxm.common;
using Dxm.login;
using Dxm.session;
using Utils;

namespace Dxm.search
{
    internal class VaccineContent : DxmContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/appointment/unFreeVaccine/list/down?";


        public VaccineContent(DxmLogin user) : base(baseUrl, user)
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            RequestUrl = $"{baseUrl}hospitalCode={hosId}";
        }
    }
}
