using Dxm.common;
using Dxm.login;
using System.Collections.Generic;
using Utils;

namespace Dxm.appointment
{
    internal class YuyueContent : DxmContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/appointmentlog/save";
        public Order Order { get; private set; }
        public YuyueContent(Order order, DxmLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("hospitalCode", Order.HospitalCode);
            AddContent("makeAnAppointment", Order.MakeAnAppointment);
            AddContent("vaccines", BuildVaccines());
            AddContent("list", BuildUserIdList());
            AddContent("phone", Order.User.Phone);
            AddContent("timeNo", Order.TimeNo);
            AddContent("timeType", Order.TimeType);
        }

        private string[] BuildUserIdList()
        {
            var userIdList = new List<string>();
            userIdList.Add(User.UserId);

            return userIdList.ToArray();
        }

        private List<Dictionary<string, object>> BuildVaccines()
        {
            var vaccineList = new List<Dictionary<string, object>>();
            var vaccineDic = new Dictionary<string, object>();
            vaccineDic.AddOrUpdate("vaccineId", Order.DeptId.ToInt());
            vaccineDic.AddOrUpdate("vaccineName", Order.DeptName);
            vaccineDic.AddOrUpdate("dose", null);
            vaccineDic.AddOrUpdate("facName", Order.FacName);
            vaccineDic.AddOrUpdate("spec", Order.Spec);
            vaccineDic.AddOrUpdate("price", Order.Price);
            vaccineDic.AddOrUpdate("priceTxt", Order.PriceTxt);
            vaccineDic.AddOrUpdate("vaccineType", Order.VaccineType);
            vaccineDic.AddOrUpdate("beginCountTime", Order.BeginCountTime);
            vaccineList.Add(vaccineDic);

            return vaccineList;
        }

    }
}
