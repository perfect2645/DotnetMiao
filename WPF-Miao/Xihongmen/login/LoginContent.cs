using Xihongmen.common;

namespace Xihongmen.login
{
    internal class LoginContent : XhmContent
    {
        private const string url = "https://yiyuan.dabannet.cn/loginNew";

        public string UserYzm { get; }
        public string UserPhone { get; }

        public LoginContent(string phone, string userPassword) : base(url)
        {
            UserPhone = phone;
            UserYzm = userPassword;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("mobile", UserPhone);
            AddContent("code", UserYzm);
            AddContent("key", "9b4fdBbFyGmrXQ1BmBQIjLAcmNbMfmQPg%2BIcLHe4G%2BKCdDc");
            AddContent("token", false);
        }
    }
}
