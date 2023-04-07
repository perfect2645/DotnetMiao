using HosFour.common;
using HosFour.login;

namespace HosFour.appointment
{
    internal class YuyueContent : HosFourContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/YuYue/Alipay_BookingApply";
        public Order Order { get; private set; }
        public YuyueContent(Order order, HosFourLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("hospitalUserID", Order.HospitalUserID);
            AddContent("resourceID", Order.ResourceID);
            AddContent("registDate", Order.RegistDate);
            AddContent("docCode", Order.DocCode);
            AddContent("hospitalID", Order.HospitalUserID);
            AddContent("docCode", Order.DocCode);
            AddContent("docName", Order.DocName);
            AddContent("docDuty", Order.DocDuty);
            AddContent("deptCode", Order.DeptCode);
            AddContent("deptName", Order.DeptName);
            AddContent("hospitalName", Order.HospitalName);
            AddContent("docPhotoPath", string.Empty);
            AddContent("extInfo", new object());
            AddContent("feeType", string.Empty);
            AddContent("t", Order.T);
        }
    }
}
