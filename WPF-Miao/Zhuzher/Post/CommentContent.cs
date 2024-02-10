using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Post
{
    internal class CommentContent : ZhuzherContent
    {
        private const string _url = "https://chaos.4009515151.com/api/zhuzher/users/me/project/posts/9301476/comments";
        public CommentContent(UserProject user) : base(_url, user)
        {

        }
    }
}
