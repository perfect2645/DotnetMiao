using Sxjk.common;
using Sxjk.login;
using System.Collections.Generic;

namespace Sxjk.appointment
{
    internal class YuyueContent : SxjkContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/reservation";
        public Order Order { get; private set; }
        public YuyueContent(Order order, SxjkLogin user) : base(baseUrl, user)
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
