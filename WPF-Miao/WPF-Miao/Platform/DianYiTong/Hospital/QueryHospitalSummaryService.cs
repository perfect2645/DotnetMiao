using HttpProcessor.Client;
using HttpProcessor.Request;
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

        public async Task<HttpDicResponse> SearchAsync(HospitalSummaryContent submitContent)
        {
            var response = await _hospitalSummaryClient.SearchAsync(submitContent);
            return response;
        }

        public void Search(HospitalSummaryContent submitContent)
        {
            _hospitalSummaryClient.Search(submitContent, PostSearch);
        }

        private void PostSearch(HttpDicResponse response)
        {
        }

        #endregion Query Hospital Summary
    }
}
