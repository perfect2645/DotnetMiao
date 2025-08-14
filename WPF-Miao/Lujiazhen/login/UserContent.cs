using Lujiazhen.common;
using Utils;
using Utils.datetime;

namespace Lujiazhen.login
{
    internal class UserContent : LujiazhenContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/family_list";
        public UserContent(LujiazhenLogin user) : base(baseUrl, user)
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
