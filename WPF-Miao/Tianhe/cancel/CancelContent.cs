using Tianhe.common;
using Tianhe.login;

namespace Tianhe.cancel
{
    internal class CancelContent : TianheContent
    {
        private static string url = "https://ldsq.ldrmyy120.com/rest/v1/api/examine/vaccine_cancel/";

        public CancelContent(TianheLogin user) : base(url, user)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("id", User.YuyueId);
        }
    }
}
