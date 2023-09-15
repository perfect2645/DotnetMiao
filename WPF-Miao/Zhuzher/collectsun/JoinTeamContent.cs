using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.datetime;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.collectsun
{
    internal class JoinTeamContent : HttpStringContent
    {
        private const string _url = "https://z.onewo.com/market/api/activity/team/join";
        protected UserProject User { get; set; }

        public JoinTeamContent(UserProject user) : base(_url)
        {
            User = user;
            BuildHeader();
            BuildContent();
        }


        private void BuildHeader()
        {
            AddHeader("Host", "z.onewo.com");
            AddHeader("Origin", "https://enterprise.4009515151.com");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 xxxx2023032801 vanke_app_version/5.4.66 X_API_VERSION/20230328 vanke_app/zhuzher vanke_jsbridge_version/5.4.66");
            AddHeader("Authorization", User.Authorization);
            AddHeader("Referer", "https://enterprise.4009515151.com/");  
        }

        private void BuildContent()
        {
            AddContent("projectCode", User.ProjectCode);
            AddContent("activityId", ZhuzherSession.ActivityId);
            AddContent("inviteCode", ZhuzherSession.InviteCode);
        }
    }
}
