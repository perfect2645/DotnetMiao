using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Post
{
    internal class PostArticleContent : OnewoContent
    {
        private const string _baseUrl = "https://z.onewo.com/talento/api/note/page/myNote?current=3&size=20&total=0&selfFlag=0";

        public int PostActivityId { get; set; }

        public PostArticleContent(UserProject user, int postActivityId) : base(_baseUrl, user)
        {
            BuildUrl();
            PostActivityId = postActivityId;
        }

        private void BuildUrl()
        {
            if (PostActivityId == 0)
            {
                return;
            }
            RequestUrl = $"{_baseUrl}&activityId={PostActivityId}";
        }
    }
}

