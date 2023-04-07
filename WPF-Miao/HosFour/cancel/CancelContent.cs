using HosFour.common;
using HosFour.login;

namespace HosFour.cancel
{
    internal class CancelContent : HosFourContent
    {
        private static string baseUrl = "ldrmyy120.com/rest/v1/api/examine/vaccine_cancel/";

        public CancelContent(HosFourLogin user, string cancelId) : base(baseUrl, user)
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
