using Kuerle.common;
using Kuerle.login;
using Kuerle.session;
using System;
using System.Collections.Generic;
using Utils;

namespace Kuerle.search
{
    internal class MiaoContent : KuerleContent
    {
        private static string baseUrl = "https://bzjk.qiyingtian.com/Home/CalcBindingDataSource";

        public MiaoContent(KuerleLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("CommandId", "05dd0f17-dfd4-40a2-b4ee-59128c784ab1");
            AddContent("Params", new Dictionary<string, object>());
            AddContent("options", BuildOptions());
        }

        private object BuildOptions()
        {
            var optionDic = new Dictionary<string, object>();
            optionDic.AddOrUpdate("distinct", true);

            return optionDic;
        }
    }
}
