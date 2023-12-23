using Lujiazhen.common;
using Lujiazhen.login;
using Utils;
using Utils.datetime;

namespace Lujiazhen.search
{
    internal class SignContent : LujiazhenContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v2/get_sign";
        public SignContent(LujiazhenLogin user) : base(baseUrl, user)
        {
            BuildUrl();
            BuildContent();
        }

        protected override void BuildContent()
        {
            var timestamp = DateTimeUtil.GetTimeStamp().Substring(0, 10);
            Content.AddOrUpdate("timestamp", timestamp);
            AddContent("token", User.Token);

            base.BuildContent();
        }
    }
}