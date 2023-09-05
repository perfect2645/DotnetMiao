using Dongxihu.common;
using Dongxihu.login;

namespace Dongxihu.cancel
{
    internal class CancelContent : DongxihuContent
    {
        private static string baseUrl = "ldrmyy120.com/rest/v1/api/examine/vaccine_cancel/";

        public CancelContent(DongxihuLogin user, string cancelId) : base(baseUrl, user)
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
