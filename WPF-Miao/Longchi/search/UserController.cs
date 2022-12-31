using HttpProcessor.Client;
using Longchi.common;
using Longchi.session;
using System.Net.Http;
using System.Threading.Tasks;

namespace Longchi.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(string cookie)
        {
            Task.Factory.StartNew(() => GetUser(cookie));
        }

        private void GetUser(string cookie)
        {
            var url = $"http://hpv_ym.zzytrj.net:15003/api/yuyue.php?cmd=get_user_list";
            var content = new LongchiContent(url, cookie);
            content.BuildDefaultHeaders(Client);
            var response = GetStringAsync(content).Result;
            if (response?.Body == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                return;
            }
        }

        private void SaveUser()
        {

        }
    }
}
