using Huangdai.common;
using Huangdai.login;

namespace Huangdai.cancel
{
    internal class CancelContent : HuangdaiContent
    {
        private static string url = "https://ldsq.ldrmyy120.com/rest/v1/api/examine/vaccine_cancel/";

        public CancelContent(HuangdaiLogin user, string cancelId) : base(url, user)
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
