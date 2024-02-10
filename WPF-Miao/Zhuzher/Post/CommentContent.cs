using System;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Post
{
    internal class CommentContent : ZhuzherContent
    {
        private const string _url = "https://chaos.4009515151.com/api/zhuzher/users/me/project/posts/[]/comments";
        public string PostContent { get; set; }
        public CommentContent(UserProject user, string postId, string postContent) : base(_url, user)
        {
            PostContent = postContent;
            BuildUrl(postId);
            BuildContent();
        }

        private void BuildUrl(string postId)
        {
            RequestUrl = _url.Replace("[]", postId);
        }

        private void BuildContent()
        {
            AddContent("content", PostContent);
        }
    }
}
