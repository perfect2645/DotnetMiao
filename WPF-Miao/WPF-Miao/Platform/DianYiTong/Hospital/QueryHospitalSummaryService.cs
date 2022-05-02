using HttpProcessor.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.DianYiTong.Hospital
{
    internal class QueryHospitalSummaryService
    {
        private HttpClient _hospitalSummaryClient;

        public QueryHospitalSummaryService(HttpClient hospitalSummaryClient)
        {
            _hospitalSummaryClient = hospitalSummaryClient;
        }

        #region Query Hospital Summary

        public async Task<string> SearchAsync(HospitalSummaryContent submitContent)
        {
            var resMessage = await _hospitalSummaryClient.SearchAsync(submitContent);
            return resMessage;
        }

        public void Search(HospitalSummaryContent submitContent)
        {
            _hospitalSummaryClient.Search(submitContent, PostSearch);
        }

        private void PostSearch(string response)
        {

        }

        #endregion Query Hospital Summary
    }
}
