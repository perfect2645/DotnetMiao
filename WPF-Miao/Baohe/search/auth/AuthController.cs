using Baohe.session;
using HttpProcessor.Client;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

namespace Baohe.search.auth
{
    internal class AuthController : HttpClientBase
    {
        public AuthController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetAuthAsync()
        {
            Task.Factory.StartNew(() =>
            {
                GetAuth();
            });
        }

        private void GetAuth()
        {
            BaoheSession.PlatformSesstion.Add("YiHu_OpenId", "eyJPcGVuSUQiOiJvZDBBandKMGVvZERZWUE0cFVmRFJVQ0lJbThFIiwiU2VjU3RyIjoiQjQwQTEzOTVGQjI1RkQ2MzRDRDkyNjA2MkM2M0UxQTUifQ%3D%3D");
            BaoheSession.PlatformSesstion.Add("logintype", "62");
            BaoheSession.PlatformSesstion.Add("loginprovinceid", "0");
            BaoheSession.PlatformSesstion.Add("logincityid", "0");
            BaoheSession.PlatformSesstion.Add("loginid", "od0AjwJ0eodDYYA4pUfDRUCIIm8E");
            BaoheSession.PlatformSesstion.Add("OpenID", "od0AjwJ0eodDYYA4pUfDRUCIIm8E");
            BaoheSession.PlatformSesstion.Add("BaseDoctorUid", "0");
            BaoheSession.PlatformSesstion.Add("BaseUserType", "0");
            BaoheSession.PlatformSesstion.Add("LoginChannel", "9000370");
            BaoheSession.PlatformSesstion.Add("YiHu_UserJosn", "eyJBY2NvdW50U24iOiIxNDg1MjgxMjkiLCJDYXJkTnVtYmVyIjoiMjA3ODYzMTUzMyIsIkxvZ2luSWQiOiJvZDBBandKMGVvZERZWUE0cFVmRFJVQ0lJbThFIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiI4MEI0RUFEN0NDOTY1M0YxQkQ1MjVBM0VGNEM5MkNGRCJ9");
            BaoheSession.PlatformSesstion.Add("TOKEN_5D0161F2EB225D58BD7D4CE01260C0E2", "C600554136584217A36C13CF9683F91E");
        }

        public void GetCookieAdvance(string cookie)
        {
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return;
            }

            var dic = cookie.CookieToDic();

            BaoheSession.PlatformSesstion.AddOrUpdate(dic);
        }
    }
}
