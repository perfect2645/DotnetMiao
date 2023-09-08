using Jingjiang.common;
using Jingjiang.login;
using Jingjiang.session;
using Utils;
using Utils.datetime;

namespace Jingjiang.search
{
    internal class MiaoContent : JingjiangContent
    {
        private static string baseUrl = "http://yygh.well-bone.com/prod-api/system/SysYwDatetime/queryListByYwId/";

        public MiaoContent(JingjiangLogin user) : base(baseUrl, user)
        {
            BuildUrl();
        }
    }
}
