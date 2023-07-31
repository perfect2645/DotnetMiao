using Yongding.common;

namespace Yongding.login
{
    internal class UserContent : YongdingContent
    {
        private static string baseUrl = "http://yiliao2.lefeiniu.com:8081/resource/card_list?token=";
        public UserContent(YongdingLogin user) : base(baseUrl, user)
        {
            RequestUrl = $"{baseUrl}{user.Token}";
        }
    }
}
