using Baohe.constants;
using Baohe.session;
using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.baseClasses
{
    internal class ContentBase : HttpStringContent
    {

        public ContentBase(string url) : base(url)
        {
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            AddHeader("Host", "appoint.yihu.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "https://appoint.yihu.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36");
            //AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");

            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            base.BuildDefaultHeaders(httpClient);
        }

        public virtual string BuildCookie()
        {
            var sb = new StringBuilder();
            sb.BuildSession(Constant.YihuOpenId);
            sb.BuildSession(Constant.LoginType);
            sb.BuildSession(Constant.LoginProvinceiId);
            sb.BuildSession(Constant.LoginCityId);
            sb.BuildSession(Constant.LoginId);
            sb.BuildSession(Constant.OpenId);
            sb.BuildSession(Constant.BaseDoctorUid);
            sb.BuildSession(Constant.BaseUserType);
            sb.BuildSession(Constant.LoginChannel);
            sb.BuildSession(Constant.YiHuUserJosn);

            return sb.ToString();
        }
    }
}
