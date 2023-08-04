using Dxm.common;
using Dxm.login;
using Dxm.session;
using System;
using System.Collections.Generic;
using Utils;

namespace Dxm.search
{
    internal class DateContent : DxmContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/appointment/date";
        public string VaccineId { get;}

        public DateContent(DxmLogin user, string vaccineId) : base(baseUrl, user)
        {
            VaccineId = vaccineId;
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            AddContent("hospitalCode", hosId);
            AddContent("vaccines", BuildVaccines());
            AddContent("year", DateTime.Today.Year);

            var dayOfMonth = DateTime.Today.Day;
            var month = DateTime.Today.Month;
            if (dayOfMonth > 28)
            {
                month += 1;
            }
            AddContent("month", month);
        }

        private List<Dictionary<string, object>> BuildVaccines()
        {
            var vaccineList = new List<Dictionary<string, object>>();
            var vaccineDic = new Dictionary<string, object>();
            vaccineDic.AddOrUpdate("vaccineId", VaccineId.ToInt());
            vaccineDic.AddOrUpdate("vaccineType", 2);
            vaccineList.Add(vaccineDic);

            return vaccineList;
        }
    }
}
