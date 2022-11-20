using HttpProcessor.Content;

namespace suiyang.common
{
    internal class SuiyangBaseContent : HttpStringContent
    {
        public SuiyangBaseContent(string url) : base(url)
        {
        }
    }
}
