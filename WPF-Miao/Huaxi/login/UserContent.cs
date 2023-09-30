using Huaxi.common;
using Huaxi.login;
using Huaxi.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;

namespace Huaxi.login
{
    internal class UserContent : HuaxiContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/family_list";
        public UserContent(HuaxiLogin user) : base(baseUrl, user)
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
