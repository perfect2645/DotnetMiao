using Xihongmen.common;

namespace Xihongmen.login
{
    internal class YzmContent : XhmContent
    {
        private const string url = "https://yiyuan.dabannet.cn/sendCode";

        public string UserPhone { get; }

        public YzmContent(string phone) : base(url)
        {
            UserPhone = phone;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("mobile", UserPhone);
            AddContent("type_id", "3");
            AddContent("key", Key);
            AddContent("token", false);
        }
    }
}
