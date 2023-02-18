using Gangwu.common;
using System;
using System.Collections.Generic;

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


            return input;
        }
    }
}
