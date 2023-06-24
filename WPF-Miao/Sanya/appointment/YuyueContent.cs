using Sanya.common;
using Sanya.login;
using System.Collections.Generic;

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
        }

        private void BuildContent()
        {
            AddContent("family_id", Order.FamilyId);
            AddContent("hos_id", Order.HosId);
            AddContent("idcard", Order.IdCard);
            AddContent("name", Order.UserName);
            AddContent("num_id", Order.NumId);
            AddContent("openid", Order.OpenId);
            AddContent("phone", Order.Phone);
            AddContent("project_id", Order.ProjectId);
            AddContent("register_name", Order.UserName);
            AddContent("sch_id", Order.SchId);
            AddContent("sex", 2);
            AddContent("time", Order.Time);
        }
    }
}
