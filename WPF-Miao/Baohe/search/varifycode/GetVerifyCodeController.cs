using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Baohe.search.varifycode
{
    internal class GetVerifyCodeController : HttpClientBase
    {
        public GetVerifyCodeController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetVerifyCodeAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => GetVerifyCode(sessionItem));
        }

        private void GetVerifyCode(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/registerAuth/showVerifyCode?t=1";
            var content = new GetVerifyCodeContent(url, sessionItem);
            content.AddHeader("Cookie", sessionItem.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            var response = GetStringAsync(content).Result;

            sessionItem.SessionDic.AddOrUpdate(Constant.VerifyCode, response);
        }
    }
}
