using Dxm.common;
using Dxm.login;

namespace Dxm.cancel
{
    internal class CancelContent : DxmContent
    {
        private static string baseUrl = "ldrmyy120.com/rest/v1/api/examine/vaccine_cancel/";

        public CancelContent(DxmLogin user, string cancelId) : base(baseUrl, user)
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
