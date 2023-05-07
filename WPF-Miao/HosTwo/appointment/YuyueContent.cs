using HosTwo.common;
using HosTwo.login;

namespace HosTwo.appointment
{
    internal class YuyueContent : HosTwoContent
    {
        private static string baseUrl = "https://fwcs.linkingcloud.cn/YuYue/Alipay_BookingApply";
        public Order Order { get; private set; }
        public YuyueContent(Order order, HosTwoLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("hospitalUserID", Order.HospitalUserID);
            AddEncodeContent("resourceID", Order.ResourceID);
            AddEncodeContent("registDate", Order.RegistDate);
            AddEncodeContent("url", Order.Url);
            AddEncodeContent("docCode", Order.DocCode);
            AddEncodeContent("hospitalID", Order.HospitalID);
            AddEncodeContent("docCode", Order.DocCode);
            AddEncodeContent("docName", Order.DocName);
            AddEncodeContent("docDuty", Order.DocDuty);
            AddEncodeContent("deptCode", Order.DeptCode);
            AddEncodeContent("deptName", Order.DeptName);
            AddEncodeContent("hospitalName", Order.HospitalName);
            AddContent("docPhotoPath", string.Empty);
            AddContent("extInfo", "{}");
            AddContent("feeType", string.Empty);
            AddContent("t", Order.T);
        }
    }
}
