using Jksx.common;
using Jksx.login;

namespace Jksx.cancel
{
    internal class CancelContent : JksxContent
    {
        private static string baseUrl = "ldrmyy120.com/rest/v1/api/examine/vaccine_cancel/";

        public CancelContent(JksxLogin user, string cancelId) : base(baseUrl, user)
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
