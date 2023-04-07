using Shaoguan.common;
using Shaoguan.login;

namespace Shaoguan.cancel
{
    internal class CancelContent : MainContent
    {
        private static string url = "https://ldsq.ldrmyy120.com/rest/v1/api/examine/vaccine_cancel/";

        public CancelContent(ShaoguanLogin user, string cancelId) : base(url, user)
        {
            CancelId = cancelId;
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        public string CancelId { get; }

        private void BuildContent()
        {
            AddContent("id", CancelId);
        }
    }
}
