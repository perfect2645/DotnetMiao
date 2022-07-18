using HttpProcessor.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.yunnan.getUserInfo
{
    internal class UserInfoController : HttpClientBase
    {
        public UserInfoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<string> GetUserInfo()
        {
            var content = 
            var searchResponse = await SearchAsync(timeUrl);
            return new HeaderTimestamp(searchResponse);
        }
    }
}
