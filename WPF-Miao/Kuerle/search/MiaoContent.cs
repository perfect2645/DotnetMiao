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
            var pidKey = MainSession.PlatformSession.GetString(Constants.PidKey);
            AddContent("CommandId", pidKey);
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
