using Utils;
using Utils.datetime;
using Xihongmen.common;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class MiaoContent : XhmContent
    {
        private const string url = "https://yiyuan.dabannet.cn/appointment";

        public MiaoContent() : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("type_id", MainSession.PlatformSession.GetString(Constants.DeptId));

            var timeStamp = DateTimeUtil.GetTimeStamp();
            AddContent("time", timeStamp);
            AddContent("key", Key);
            AddContent("token", MainSession.Token);
        }
    }
}
