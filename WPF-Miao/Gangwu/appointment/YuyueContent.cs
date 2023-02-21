using Gangwu.common;
using Gangwu.session;
using System;
using System.Collections.Generic;
using Utils;

namespace Gangwu.appointment
{
    internal class YuyueContent : GangwuContent
    {
        private static string url = "https://health-cn.xyz:9033/health_xcdp/api/hpv/save";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            BuildTopLevel();
        }

        private void BuildTopLevel()
        {
            AddContent("operationName", "CreatePublishedFormEntry");
            AddContent("variables", BuildVariables());
            AddContent("extensions", BuildExtensions());
        }

        private Dictionary<string, object> BuildVariables()
        {
            var variables = new Dictionary<string, object>();
            variables.Add("input", BuildInput());
            return variables;
        }

        private Dictionary<string, object> BuildInput()
        {
            var input = new Dictionary<string, object>();
            input.AddOrUpdate("formId", MainSession.PlatformSession.GetString(Constants.HospitalId));
            input.AddOrUpdate("entryAttributes", BuildEntryAttributes());
            input.AddOrUpdate("captchaData", null);
            input.AddOrUpdate("weixinAccessToken", null);
            input.AddOrUpdate("xFieldWeixinOpenid", null);
            input.AddOrUpdate("weixinInfo", null);
            input.AddOrUpdate("prefilledParams", string.Empty);
            input.AddOrUpdate("embedded", false);
            input.AddOrUpdate("internal", false);
            input.AddOrUpdate("backgroundImage", false);
            input.AddOrUpdate("formMargin", false);
            input.AddOrUpdate("hasPreferential", false);
            input.AddOrUpdate("fillingDuration", 177.418);
            input.AddOrUpdate("forceSubmit", false);

            return input;
        }

        private Dictionary<string, object> BuildEntryAttributes()
        {
            var entryAttributes = new Dictionary<string, object>();
            entryAttributes.AddOrUpdate("field_1", MainSession.PlatformSession.GetString(Constants.DeptId));
            entryAttributes.AddOrUpdate("field_2", Order.UserName);
            entryAttributes.AddOrUpdate("field_3", "jwPD"); // 性别：女
            entryAttributes.AddOrUpdate("field_4", Order.Age);
            entryAttributes.AddOrUpdate("field_5", Order.Phone);
            entryAttributes.AddOrUpdate("field_6", Order.Email);
            entryAttributes.AddOrUpdate("field_12", "FxQ5");

            return entryAttributes;
        }

        private Dictionary<string, object> BuildExtensions()
        {
            var extensions = new Dictionary<string, object>();
            extensions.Add("persistedQuery", BuildPersistedQuery());
            return extensions;
        }

        private object BuildPersistedQuery()
        {
            var persistedQuery = new Dictionary<string, object>();
            persistedQuery.Add("version", 1);
            persistedQuery.Add("sha256Hash", 1);
            return persistedQuery;
        }
    }
}
