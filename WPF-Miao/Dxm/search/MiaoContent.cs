using Dxm.common;
using Dxm.login;
using Dxm.session;
using System.Collections.Generic;
using Utils;

namespace Dxm.search
{
    internal class MiaoContent : DxmContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/appointment/details";

        public string Date { get; set; }

        public MiaoContent(DxmLogin user, string date) : base(baseUrl, user)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("hospitalCode", hosId);
            AddContent("makeAnAppointment", Date);
            AddContent("vaccines", BuildVaccines());
        }

        private List<Dictionary<string, object>> BuildVaccines()
        {
            var vaccineList = new List<Dictionary<string, object>>();
            var vaccineDic = new Dictionary<string, object>();

            var vaccineId = MainSession.PlatformSession.GetString(Constants.DeptId);

            vaccineDic.AddOrUpdate("vaccineId", vaccineId.ToInt());
            vaccineDic.AddOrUpdate("vaccineType", 2);
            vaccineList.Add(vaccineDic);

            return vaccineList;
        }
    }
}
