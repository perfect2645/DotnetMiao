using Jingjiang.common;
using Jingjiang.login;
using Jingjiang.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;

namespace Jingjiang.login
{
    internal class UserContent : JingjiangContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/family_list";
        public UserContent(JingjiangLogin user) : base(baseUrl, user)
        {
            BuildUrl();
            BuildContent();
        }

        protected override void BuildContent()
        {
            AddContent("page", 1);
            var timestamp = DateTimeUtil.GetTimeStamp().Substring(0, 10);
            Content.AddOrUpdate("timestamp", timestamp);
            AddContent("token", User.Token);

            base.BuildContent();
        }
    }
}
