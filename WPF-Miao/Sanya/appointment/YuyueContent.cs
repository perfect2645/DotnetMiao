using Sanya.common;
using Sanya.login;
using System.Collections.Generic;
using Utils;

namespace Sanya.appointment
{
    internal class YuyueContent : SanyaContent
    {
        private static string baseUrl = "https://public.health.zoenet.cn/business-service/mask-appointment/save";
        public Order Order { get; private set; }
        public YuyueContent(Order order, SanyaLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
            BuildHeaderSign();
        }

        private void BuildContent()
        {
            AddContent("name", Order.UserName);
            AddContent("idCardNo", Order.IdCardNo);
            AddContent("icCardNo", Order.IcCardNo);
            AddContent("goodsId", Order.GoodsId);
            AddContent("orgCode", Order.OrgCode);
            AddContent("timeStr", Order.TimeStr);
            AddContent("serviceId", Order.ServiceId);
            AddContent("goodsDetailId", Order.GoodsDetailId);
            AddContent("subscribeType", Order.SubscribeType);
            AddContent("provideAddress", Order.ProvideAddress);
            AddContent("certificateType", string.Empty);
            AddContent("transactTypeCode", string.Empty);
            AddContent("transactTypeName", string.Empty);
            AddContent("specialRegistrationCode", string.Empty);
            AddContent("specialRegistrationName", string.Empty);
            AddContent("nationality", string.Empty);
            AddContent("businessContent", string.Empty);
            AddContent("sex", string.Empty);
            AddContent("telephone", string.Empty);
            AddContent("babyName", string.Empty);
            AddContent("babyRelation", string.Empty);
            AddContent("babyBirthday", string.Empty);
            AddContent("age", Order.Age);
        }

        protected override void BuildHeaderSign()
        {
            foreach(var content in Content)
            {
                HeaderSignDic.AddOrUpdate(content.Key, content.Value);
            }
            base.BuildHeaderSign();
        }
    }
}
