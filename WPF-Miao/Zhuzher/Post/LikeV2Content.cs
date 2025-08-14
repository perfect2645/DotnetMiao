using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhuzher.Common;
using Zhuzher.search;

namespace Zhuzher.Post
{
    internal class LikeV2Content : OnewoContent
    {
        private const string _url = "https://z.onewo.com/talento/api/thumbUp/add";
        public int NoteId { get; private set; } 
        public LikeV2Content(UserProject user, int noteId) : base(_url, user)
        {
            NoteId = noteId;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("noteId", NoteId);
        }
    }
}
