namespace Tianhe.login
{
    internal class TianheLogin
    {
        public string code { get; set; }
        public string encrypted_data { get; set; }
        public string signature { get; set; }
        public string iv { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }
        public string Token { get; set; }
    }
}
