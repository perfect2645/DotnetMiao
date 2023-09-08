using HttpProcessor.Content;
using Jingjiang.login;
using Jingjiang.session;
using System;
using System.Security.Policy;
using System.Text;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Jingjiang.common
{
    internal class JingjiangContent : HttpStringContent
    {
        public JingjiangLogin User { get; private set; }
        public string BaseUrl { get; set; }

        public JingjiangContent(string baseUrl, JingjiangLogin user) : base(baseUrl)
        {
            User = user;
            BaseUrl = baseUrl;
        }

        private void BuildHeader()
        {
            AddHeader("Host", $"yygh.well-bone.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("Content-Language", "zh_CN");
            AddHeader("Referer", "http://yygh.well-bone.com/system/SysYwBookDetail");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }

        protected void BuildUrl()
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            RequestUrl = $"{BaseUrl}{deptId}";
        }
    }
}
