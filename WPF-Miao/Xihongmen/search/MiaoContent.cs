using Utils;
using Utils.datetime;
using Xihongmen.common;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class MiaoContent : XhmContent
    {
        private const string url = "https://yiyuan.dabannet.cn/appointment";

        public string Time { get; private set; }

        public MiaoContent(string time) : base(url)
        {
            Time = time;
            BuildContent();
        }

        public MiaoContent() : base(url)
        {
        }

        internal void BuildContent(string time)
        {
            AddContent("type_id", MainSession.PlatformSession.GetString(Constants.DeptId));

            AddContent("time", time);
            AddContent("key", Key);
            AddContent("token", MainSession.Token);
        }

        internal void BuildContent()
        {
            AddContent("type_id", MainSession.PlatformSession.GetString(Constants.DeptId));

            AddContent("time", Time);
            AddContent("key", Key);
            AddContent("token", MainSession.Token);
        }
    }
}
