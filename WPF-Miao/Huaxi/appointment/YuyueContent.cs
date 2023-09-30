using Huaxi.common;
using Huaxi.login;
using Huaxi.session;
using System;
using Utils;
using Utils.datetime;

namespace Huaxi.appointment
{
    internal class YuyueContent : HuaxiContent
    {
        private static string url = "https://mcpwxp.motherchildren.com/cloud/appointment/publicClient/sureOrder";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        protected override void BuildContent()
        {
            AddContent("organCode", Order.OrganCode);
            AddContent("appCode", Order.AppCode);
            AddContent("channelCode", Order.ChannelCode);
            AddContent("cardId", Order.CardId);
            AddContent("scheduleId", Order.ScheduleId);
            //AddContent("sureOrderVerify", Order.SureOrderVerify);
            //AddContent("sureOrderVerifyInfo", Order.SureOrderVerifyInfo);
            //AddContent("sureOrderVerifyIndex", Order.SureOrderVerifyIndex);
            //AddContent("verifyImageCodeType", Order.VerifyImageCodeType);
        }
    }
}
