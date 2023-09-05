using HosTwo.common;
using HosTwo.login;

namespace HosTwo.cancel
{
    internal class CancelContent : HosTwoContent
    {
        private static string baseUrl = "ldrmyy120.com/rest/v1/api/examine/vaccine_cancel/";

        public CancelContent(HosTwoLogin user, string cancelId) : base(baseUrl, user)
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
