using Shengzhi.common;
using Shengzhi.login;
using Utils;

namespace Shengzhi.appointment
{
    internal class YuyueContent : WechatContent
    {
        private static string url = "https://app.quyiyuan.com/APP/appoint/action/AppointActionC.jspx";
        public Order Order { get; private set; }
        public YuyueContent(Order order, ShengzhiLogin user) : base(url, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("APPOINT_SOURCE", Order.AppointScore);
            AddContent("APP_UUID", Order.AppUuid);
            AddContent("CHANNEL_ID", Order.ChannelId);
            AddContent("GROUP_CODE", Order.GroupCode);
            AddContent("HOSPITAL_WX_OPEN_ID", Order.HospitalWxOpenId);
            AddContent("IMEI_ID", Order.ImeiId);
            AddContent("PHONEOPERATINGSYS", Order.PhoneOperationSys);
            AddContent("PHONETYPE", Order.PhoneType);
            AddContent("PHONEVERSIONNUM", Order.PhoneVersionNum);
            AddContent("PUBLIC_SERVICE_TYPE", Order.PublicServiceType);
            AddContent("QY_CHECK_SUFFIX", Order.QyCheckSuffix);
            AddContent("USER_ID", Order.UserId);
            AddContent("USER_VS_ID", Order.UserVsId);
            AddContent("hospitalID", Order.HospitalId);
            AddContent("isLogin", true);
            AddContent("loc", "c");
            AddContent("op", "monitorRecords");
            AddContent("opVersion", Order.OpVersion);
            AddContent("operateCurrent_UserId", Order.UserId);
            AddContent("operateUserSource", Order.OperateUserSource);
        }

        //private void BuildContent()
        //{
        //    AddContent("APPOINT_SOURCE", Order.AppointScore);
        //    AddContent("APP_UUID", Order.AppUuid);
        //    AddContent("CHANNEL_ID", Order.ChannelId);
        //    AddContent("GROUP_CODE", Order.GroupCode);
        //    AddContent("HOSPITAL_WX_OPEN_ID", Order.HospitalWxOpenId);
        //    AddContent("IMEI_ID", Order.ImeiId);
        //    AddContent("PHONEOPERATINGSYS", Order.PhoneOperationSys);
        //    AddContent("PHONETYPE", Order.PhoneType);
        //    AddContent("PHONEVERSIONNUM", Order.PhoneVersionNum);
        //    AddContent("PUBLIC_SERVICE_TYPE", Order.PublicServiceType);
        //    AddContent("QY_CHECK_SUFFIX", Order.QyCheckSuffix);
        //    AddContent("USER_ID", Order.UserId);
        //    AddContent("USER_VS_ID", Order.UserVsId);
        //    AddContent("hospitalID", Order.HospitalId);
        //    AddContent("isLogin", true);
        //    AddContent("loc", "c");
        //    AddContent("op", "monitorRecords");
        //    AddContent("opVersion", Order.OpVersion);
        //    AddContent("operateCurrent_UserId", Order.UserId);
        //    AddContent("operateUserSource", Order.OperateUserSource);
        //}
    }
}
