using Utils;
using Xihongmen.common;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class TimeContent : XhmContent
    {
        private const string url = "https://yiyuan.dabannet.cn/timeList";

        public TimeContent() : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("type_id", MainSession.PlatformSession.GetString(Constants.DeptId));
            AddContent("key", Key);
            AddContent("token", MainSession.Token);
        }
    }
}
