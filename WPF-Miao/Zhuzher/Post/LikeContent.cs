using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Post
{
    internal class LikeContent : ZhuzherContent
    {
        private const string _url = "https://chaos.4009515151.com/api/zhuzher/users/me/project/posts/[]/up";
        public LikeContent(UserProject user, string postId) : base(_url, user)
        {
            BuildUrl(postId);
            BuildContent();
        }

        private void BuildUrl(string postId)
        {
            RequestUrl = _url.Replace("[]", postId);
        }

        private void BuildContent()
        {
            AddContent("up", 1);
            AddContent("icon", 0);
        }
    }
}