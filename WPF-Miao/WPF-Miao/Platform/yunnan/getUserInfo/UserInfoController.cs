using HttpProcessor.Client;
using HttpProcessor.Container;
using Microsoft.Extensions.DependencyInjection;
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
            var content = HttpServiceController.ServiceProvider.GetService<UserInfoContent>();

            var searchResponse = await SearchAsync(content!);

            return searchResponse.JsonBody?.ToString();
        }
    }
}
