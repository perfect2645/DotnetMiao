using Gzjk.common;
using Gzjk.login;
using System.Collections.Generic;

namespace Gzjk.appointment
{
    internal class YuyueContent : GzjkContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/reservation";
        public Order Order { get; private set; }
        public YuyueContent(Order order, GzjkLogin user) : base(baseUrl, user)
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
