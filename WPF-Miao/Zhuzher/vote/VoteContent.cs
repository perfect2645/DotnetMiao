using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhuzher.Play;
using Zhuzher.search;

namespace Zhuzher.vote
{
    internal class VoteContent : HttpStringContent
    {
        public const string Url = "https://communityclass.onewo.com/app/api/app/userVote/add";

        private UserProject _user;

        public VoteContent(UserProject userProject) : base(Url)
        {
            _user = userProject;
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "communityclass.onewo.com");
            AddHeader("Accept", "application/json, text/plain, */*");
            var auth = _user.Authorization.Substring(_user.Authorization.IndexOf(' '));
            AddHeader("Authorization", auth);
            AddHeader("projectCode", _user.ProjectCode);
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("x-source", "zhuzher");
            AddHeader("X-XSRF-TOKEN", "47a47bdc-0d96-4315-a6cf-cd5bb7e07cc6");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Origin", "https://communityclass.onewo.com");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 xxxx2024011701 vanke_app_version/5.5.76 X_API_VERSION/20240117 vanke_app/zhuzher vanke_jsbridge_version/5.5.76");
            AddHeader("Referer", "https://communityclass.onewo.com/");
            AddHeader("Connection", "keep-alive");

        }

        private void BuildContent()
        {
            //3728 fawei
            // 3706 dalian
            AddContent("worksId", 3728);
            AddContent("voteId", 27);
        }
    }
}