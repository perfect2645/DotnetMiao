using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Post
{
    internal class CommentV2Content : OnewoContent
    {
        private const string _url = "https://z.onewo.com/talento/api/comment/add";
        public string PostContent { get; set; }
        public int NoteId { get; set; }
        public CommentV2Content(UserProject user, string noteId, string postContent) : base(_url, user)
        {
            PostContent = postContent;
            NoteId = noteId.ToInt();
            BuildContent();
        }


        private void BuildContent()
        {
            AddContent("content", PostContent);
            AddContent("noteId", NoteId);
        }
    }
}
