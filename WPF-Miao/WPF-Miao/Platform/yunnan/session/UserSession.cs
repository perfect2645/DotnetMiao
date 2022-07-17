namespace WPF_Miao.Platform.yunnan.session
{
    public class UserSession
    {
        public string Cookie { get; set; }
        public string Referer { get; set; }

        public UserSession(string cookie)
        {
            Cookie = cookie;
        }
    }
}
