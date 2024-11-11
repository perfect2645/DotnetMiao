using HttpProcessor.Content;

namespace Zhuzher.Post
{
    internal class ImageContent : HttpStringContent
    {
        public ImageContent(string url) : base(url)
        {
        }
    }
}
