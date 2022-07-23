using HttpProcessor.Client;
using System.Net.Http;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.model;

namespace WPF_Miao.Platform.yunnan.getTimestamp
{
    internal class GetTimestampController : HttpClientBase
    {
        public GetTimestampController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<HeaderTimestamp> GetTimestamp()
        {
            var timeUrl = "https://weixin.ngarihealth.com/weixin//api/timestamp";
            var searchResponse = await SearchAsync(timeUrl);
            return new HeaderTimestamp(searchResponse);
        }

        //public async Task<string> GetXCaNonce()
        //{
        //    var time = await GetTimestamp();

        //}
    }
}
