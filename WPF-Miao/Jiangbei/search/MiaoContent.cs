using Jiangbei.common;
using Jiangbei.login;
using Jiangbei.session;
using Utils;

namespace Jiangbei.search
{
    internal class MiaoContent : JiangbeiContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/peresvapi/mobile/sch/date/time_list_deatil?date=";

        public MiaoContent(JiangbeiLogin user, string date) : base(baseUrl, user)
        {
            var dateWithFormat = date.Replace(' ', '+');
            RequestUrl = $"{baseUrl}{dateWithFormat}&hos_id=10001&project_id=10001";
        }
    }
}
