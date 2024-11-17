using HttpProcessor.Container;
using HttpProcessor.Content;
using System;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Post
{
    public class Article
    {
        public int ActivityId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public UserProject User { get; set; }
        public string NickName { get; set; }
        public string CoverUrl { get; set; }
        public byte[] ImgContent { get; set; }

        public Article()
        {

        }

        public async Task<byte[]> BuildImgAsync()
        {
            var imgController = HttpServiceController.GetService<ImageController>();
            ImgContent = await imgController.GetImageAsync(CoverUrl, User);
            return ImgContent;
        }
    }
}
